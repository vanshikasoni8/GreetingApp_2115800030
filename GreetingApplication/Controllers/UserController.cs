using BussinessLayer.Interface;
using Microsoft.AspNetCore.Mvc;
using Modellayer.Model;

namespace GreetingApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;


        public UserController(IUserBL userBL)
        {
            _userBL = userBL;
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
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            //return Ok("User registered successfully!");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            bool isValidUser = _userBL.Login(model);  // Assuming _authService has Login logic

            if (isValidUser)
            {
                return Ok("Login successful!");
            }

            return Unauthorized("Invalid credentials");
        }

        //[HttpPost("forgot-password")]
        //public IActionResult ForgotPassword([FromBody] ForgotPasswordModel model)
        //{
        //    var response = _userBL.ForgotPassword(model.Email);
        //    return Ok(response);
        //}

        //[HttpPost("reset-password")]
        //public IActionResult ResetPassword([FromBody] ResetPasswordModel model)
        //{
        //    var response = _userBL.ResetPassword(model.Email, model.NewPassword, model.Token);
        //    return Ok(response);
        //}

    }

}
