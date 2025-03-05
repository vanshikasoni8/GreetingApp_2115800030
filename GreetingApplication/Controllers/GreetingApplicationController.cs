using BussinessLayer.Interface;
using BussinessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Modellayer.Model;
using RepositaryLayer.Entity;
using RepositaryLayer.Interface;

namespace GreetingApplication.Controllers
{
    /// <summary>
    /// Class Providing API for HelloGreeting
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class GreetingApplicationController : ControllerBase
    {
        private static Dictionary<string, string> greetings = new Dictionary<string, string>();
        private readonly IGreetingBL _greetingService;
        private readonly IGreetingRL _greetingRL;

        public GreetingApplicationController(IGreetingBL greetingService, IGreetingRL greetingRL)
        {
            _greetingService = greetingService;
            _greetingRL = greetingRL;
        }


        /// <summary>
        /// Get Method to get the Greeting Message
        /// </summary>
        /// <returns>Hello, World</returns>
        [HttpGet]
        [Route("printmessage")]
        public IActionResult Get()
        {
            ResponseBody<Dictionary<string, string>> ResponseModel = new ResponseBody<Dictionary<string, string>>();

            ResponseModel.Success = true;
            ResponseModel.Message = "Hello to Greeting App API Endpoint";
            ResponseModel.Data = greetings;

            return Ok(ResponseModel);
        }

        [HttpPost]
        [Route("AddingData")]
        public IActionResult Post(RequestBody requestModel)
        {
            ResponseBody<string> ResponseModel = new ResponseBody<string>();

            greetings[requestModel.Key] = requestModel.Value;

            ResponseModel.Success = true;
            ResponseModel.Message = "Request received successfully";
            ResponseModel.Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}";

            return Ok(ResponseModel);
        }

        [HttpPut]
        public IActionResult Put([FromBody] RequestBody requestModel)
        {
            ResponseBody<Dictionary<string, string>> ResponseModel = new ResponseBody<Dictionary<string, string>>();

            // Add or update the dictionary
            greetings[requestModel.Key] = requestModel.Value;

            ResponseModel.Success = true;
            ResponseModel.Message = "Greeting updated successfully";
            ResponseModel.Data = greetings;

            return Ok(ResponseModel);
        }

        [HttpPatch]
        public IActionResult Patch(RequestBody requestModel)
        {
            ResponseBody<string> ResponseModel = new ResponseBody<string>();

            if (!greetings.ContainsKey(requestModel.Key))
            {
                ResponseModel.Success = false;
                ResponseModel.Message = "Key not found";
                return NotFound(ResponseModel);
            }

            greetings[requestModel.Key] = requestModel.Value;
            ResponseModel.Success = true;
            ResponseModel.Message = "Value partially updated successfully";
            ResponseModel.Data = $"Key: {requestModel.Key}, Updated Value: {requestModel.Value}";

            return Ok(ResponseModel);
        }

        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            ResponseBody<string> ResponseModel = new ResponseBody<string>();

            if (!greetings.ContainsKey(key))
            {
                ResponseModel.Success = false;
                ResponseModel.Message = "Key not found";
                return NotFound(ResponseModel);
            }

            greetings.Remove(key);
            ResponseModel.Success = true;
            ResponseModel.Message = "Entry deleted successfully";

            return Ok(ResponseModel);
        }

        /// <summary>
        /// Add UC2 greeting 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("greetingApp")]
        public IActionResult Greetings()
        {
            ResponseBody<string> ResponseModel = new ResponseBody<string>();

            ResponseModel.Success = true;
            ResponseModel.Message = "Greeting message fetched successfully";
            ResponseModel.Data = _greetingService.GetGreetingMessage();

            return Ok(ResponseModel);
        }

        /// <summary>
        /// Adding UC3 functionality  
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("greeting")]
        public IActionResult ByNameGreetings([FromQuery] string firstName, [FromQuery] string lastName)
        {
            var greetingMessage = _greetingService.NameGreeting(firstName, lastName);

            return Ok(new { message = greetingMessage });
        }


        [HttpGet]
        [Route("get-greetings")]
        public IActionResult GetGreetings()
        {
            ResponseBody<List<GreetingEntity>> ResponseModel = new ResponseBody<List<GreetingEntity>>();

            try
            {
                ResponseModel.Success = true;
                ResponseModel.Message = "Greetings fetched successfully";
                ResponseModel.Data = _greetingService.GetSavedGreetings();
            }
            catch (Exception ex)
            {
                ResponseModel.Success = false;
                ResponseModel.Message = $"Error : {ex.Message}";
            }

            return Ok(ResponseModel);
        }
    }
}
