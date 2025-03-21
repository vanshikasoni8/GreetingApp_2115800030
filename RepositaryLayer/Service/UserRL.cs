using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Modellayer.Context;
using Modellayer.Model;
using RepositaryLayer.Entity;
using RepositaryLayer.Interface;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace RepositaryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserRL(AppDbContext dbContext, IConfiguration config)
        {
            _context = dbContext;
            _config = config;
        }

        public bool Registeration(RegisterModel registerModel)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.Email == registerModel.Email);
            if (existingUser != null)
            {
                return false;
            }

            string salt = GenerateSalt();
            string hashedPassword = HashPassword(registerModel.Password, salt);

            UserEntity userEntity = new UserEntity
            {
                Email = registerModel.Email,
                Name = registerModel.Name,
                PasswordHash = hashedPassword,
                Salt = salt, // Store the salt in the database
                ResetToken = ""
            };

            _context.User.Add(userEntity);
            _context.SaveChanges();
            return true;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = saltBytes.Concat(passwordBytes).ToArray();
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public string Login(LoginModel loginModel)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == loginModel.Email);
            if (user == null)
            {
                return "NO USER FOUND!!"; // User not found
            }

            // Verify the password using stored salt
            string hashedInputPassword = HashPassword(loginModel.Password, user.Salt);
            if (hashedInputPassword == user.PasswordHash)
            {
                // Login successful
                return GenerateJwtToken(user.Id, user.Email);
            }

            return "INVALID CREDENTIALS"; // Invalid credentials
        }

        public string GenerateJwtToken(int userId, string Email)
        {
            var key = Encoding.UTF8.GetBytes(_config["JwtSettings:Secret"]!);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, Email)
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
