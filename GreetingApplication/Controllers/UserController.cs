using BussinessLayer.Helper;
using BussinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Modellayer.Model;

namespace GreetingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;
        private readonly PasswordService _passwordService;

        public UserController(IUserBL userBL, PasswordService passwordService)
        {
            _userBL = userBL;
            _passwordService = passwordService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            try
            {
                bool registered = _userBL.Registeration(model);
                if (registered)
                {
                    return Ok($"{model.Name} registered successfully");
                }
                else
                {
                    return BadRequest($"{model.Email} already exists");
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, $"Database Error: {dbEx.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            var isValidUser = _userBL.Login(model);  // Assuming _authService has Login logic

            return Ok(isValidUser);
        }

        [HttpPost("forgotpassword")]
        public IActionResult ForgotPassword([FromBody] ForgotPassword model)
        {
            var success = _passwordService.ForgotPassword(model.Email);
            if (!success)
            {
                return BadRequest("User not found or unable to process request.");
            }

            return Ok("Password reset link sent to your email.");
        }

        // Reset Password - Validate Token & Set New Password
        [HttpPost("resetpassword")]
        public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        {
            var success = _passwordService.ResetPassword(model.Token, model.NewPassword);
            if (!success)
            {
                return BadRequest("Invalid or expired reset token.");
            }

            return Ok("Password reset successfully.");
        }

    }

}
