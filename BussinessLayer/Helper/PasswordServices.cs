using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Modellayer.Context;

namespace BussinessLayer.Helper
{
    public class PasswordService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public PasswordService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Method to Generate Reset Token
        public string GenerateResetToken()
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenBytes = new byte[32];
                rng.GetBytes(tokenBytes);
                return Convert.ToBase64String(tokenBytes);
            }
        }

        // Method to Send Reset Token (Assuming Email is integrated)
        public void SendResetEmail(string email, string token)
        {
            // Integrate your email sending logic here
            // e.g., SMTP or a third-party service like SendGrid
        }

        // Forgot Password (Generate Token & Send Email)
        public bool ForgotPassword(string email)
        {
            var user = _context.User.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                return false; // User not found
            }

            var resetToken = GenerateResetToken();
            user.ResetToken = resetToken;
            user.ResetTokenExpiry = DateTime.Now.AddHours(1); // Token expires in 1 hour

            _context.SaveChanges();

            // Send reset token to user's email
            SendResetEmail(email, resetToken);
            return true;
        }

        // Reset Password (Validate Token & Set New Password)
        public bool ResetPassword(string token, string newPassword)
        {
            var user = _context.User.FirstOrDefault(u => u.ResetToken == token);
            if (user == null || user.ResetTokenExpiry < DateTime.Now)
            {
                return false; // Token invalid or expired
            }

            // Hash the new password
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(newPassword, salt);

            user.PasswordHash = hashedPassword;
            user.ResetToken = null; // Invalidate the token after use
            user.ResetTokenExpiry = null;

            _context.SaveChanges();
            return true;
        }

        // Helper Methods for Salt & Hashing
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
    }
}