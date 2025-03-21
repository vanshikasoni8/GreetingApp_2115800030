//using AddressBookAuth.Data;
//using AddressBookAuth.DTOs;
//using AddressBookAuth.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
//using Microsoft.AspNet.Identity;
using Microsoft.Extensions.Configuration;
using Modellayer.Context;
using RepositaryLayer.Entity;
using Modellayer.Model;

namespace AddressBookAuth.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<string?> RegisterUser(RegisterModel dto)
        {
            if (await _context.User.AnyAsync(u => u.Email == dto.Email))
                return null; // Email already exists

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new UserEntity { Name = dto.Name, Email = dto.Email, PasswordHash = hashedPassword };
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return GenerateJwtToken(user);
        }

        public async Task<string?> LoginUser(LoginModel dto)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return null;

            return GenerateJwtToken(user);
        }

        private string GenerateJwtToken(UserEntity user)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["JwtSettings:Issuer"],
                audience: _config["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:ExpirationInMinutes"])),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}