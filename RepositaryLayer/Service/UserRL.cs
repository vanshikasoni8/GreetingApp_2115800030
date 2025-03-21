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

namespace RepositaryLayer.Service
{
    public class UserRL : IUserRL
    {

        private readonly AppDbContext _context;
        

        public UserRL(AppDbContext dbContext)
        {
            _context = dbContext;
            
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
                Salt = salt // Store the salt in the database
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
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] combinedBytes = saltBytes.Concat(passwordBytes).ToArray();
                byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool Login(LoginModel loginModel)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == loginModel.Email);
            if (user == null)
            {
                return false; // User not found
            }

            // Verify the password using stored salt
            string hashedInputPassword = HashPassword(loginModel.Password, user.Salt);
            if (hashedInputPassword == user.PasswordHash)
            {
                return true; // Login successful
            }

            return false; // Invalid credentials
        }

        //public string ForgotPassword(string email)
        //{
        //    var user = _userManager.FindByEmailAsync(email).Result;
        //    if (user == null)
        //    {
        //        return "User not found!";
        //    }

        //    var resetToken = _userManager.GeneratePasswordResetTokenAsync(user).Result;

        //    // Send reset token via email (implementation needed)
        //    return $"Password reset token: {resetToken}";
        //}

        //public string ResetPassword(string email, string newPassword, string token)
        //{
        //    var user = _userManager.FindByEmailAsync(email).Result;
        //    if (user == null)
        //    {
        //        return "User not found!";
        //    }

        //    var result = _userManager.ResetPasswordAsync(user, token, newPassword).Result;
        //    if (!result.Succeeded)
        //    {
        //        return "Invalid or expired token!";
        //    }

        //    return "Password reset successful!";
        //}
    }
}
