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
    /// 
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase

    {

        
        private readonly ILogger<HelloGreetingController> _logger;
        private readonly IGreetingBL _greetingService;
        private readonly IGreetingRL _greetingRL;


        /// <summary>
        /// Constructor to initialize logger.
        /// </summary>
        public HelloGreetingController(ILogger<HelloGreetingController> logger, IGreetingBL greetingService, IGreetingRL greetingRL)
        {
            _logger = logger;
            _greetingService = greetingService;
            _greetingRL = greetingRL;

        }

        /// <summary>
        /// Get Method to get the greeting message
        /// </summary>
        /// <returns> Hello World!</returns>
        [HttpGet]
        [Route("get")]
        public IActionResult Get()
        {

            _logger.LogInformation("GET request received.");
            ResponseBody<string> ResponseBody = new ResponseBody<string>();
            ResponseBody.Success = true;
            ResponseBody.Message = " Hello to Greeting App API EndPoint";
            ResponseBody.Data = "Hello World!";
            return Ok(ResponseBody);
        }



        [HttpPost]
        [Route("post")]
        public IActionResult Post(RequestBody RequestBody)
        {

            _logger.LogInformation("POST request received with Key: {Key}, Value: {Value}", RequestBody.Key, RequestBody.Value);

            ResponseBody<string> ResponseBody = new ResponseBody<string>();


            ResponseBody.Success = true;
            ResponseBody.Message = "Received successfully";
            ResponseBody.Data = $"key :{RequestBody.Key},Value:{RequestBody.Value}";
            return Ok(ResponseBody);
        }



        [HttpPut]
        [Route("post")]
        public IActionResult Put(RequestBody RequestBody)
        {

            _logger.LogInformation("PUT request received with Key: {Key}, Value: {Value}", RequestBody.Key, RequestBody.Value);

            ResponseBody<string> ResponseBody = new ResponseBody<string>
            {
                Success = true,
                Message = "Updated successfully",
                Data = $"Key: {RequestBody.Key}, Value: {RequestBody.Value}"
            };

            return Ok(ResponseBody);
        }

        [HttpPatch]
        [Route("post")]
        public IActionResult Patch(RequestBody RequestBody)
        {
            _logger.LogInformation("PATCH request received with Key: {Key}, Value: {Value}", RequestBody.Key, RequestBody.Value);
            ResponseBody<string> ResponseBody = new ResponseBody<string>
            {
                Success = true,
                Message = "Partially updated successfully",
                Data = $"Key: {RequestBody.Key}, Value: {RequestBody.Value}"
            };

            return Ok(ResponseBody);
        }

        [HttpDelete]
        [Route("delete")]
        
        public IActionResult Delete(RequestBody RequestBody)
        {
            _logger.LogInformation("DELETE request received for Key: {Key}", RequestBody.Key);

            ResponseBody<string> ResponseBody = new ResponseBody<string>
            {
                Success = true,
                Message = "Deleted successfully",
                Data = $"Key: {RequestBody.Key}, Value: {RequestBody.Value}"
            };

            return Ok(ResponseBody);
        }




        /// <summary>
        /// Add UC2 greeting 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("greetingApp")]
        public IActionResult Greetings()
        {
            ResponseBody<string> ResponseBody = new ResponseBody<string>();

            ResponseBody.Success = true;
            ResponseBody.Message = "Greeting message fetched successfully";
            ResponseBody.Data = _greetingService.GetGreetingMessage();

            return Ok(ResponseBody);
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
            ResponseBody<List<GreetingEntity>> ResponseBody = new ResponseBody<List<GreetingEntity>>();

            try
            {
                ResponseBody.Success = true;
                ResponseBody.Message = "Greetings fetched successfully";
                ResponseBody.Data = _greetingService.GetSavedGreetings();
            }
            catch (Exception ex)
            {
                ResponseBody.Success = false;
                ResponseBody.Message = $"Error : {ex.Message}";
            }

            return Ok(ResponseBody);
        }

        [HttpPost]
        [Route("save-greeting")]
        public IActionResult SaveGreetings([FromBody] GreetingEntity greeting) { 
            ResponseBody<string> ResponseModel = new ResponseBody<string>();

            try
            {
                _greetingService.SaveGreetingMessage(greeting);
                ResponseModel.Success = true;
                ResponseModel.Message = "Greeting saved successfully";
                ResponseModel.Data = $"Greeting for {greeting.FirstName} {greeting.LastName} saved.";
            }
            catch (Exception ex)
            {
                ResponseModel.Success = false;
                ResponseModel.Message = $"Error saving greeting: {ex.Message}";
            }

            return Ok(ResponseModel);
            
        }


        [HttpGet]
        [Route("get-greeting/{id}")]
        public IActionResult GetGreetingById(int id)
        {
            var greeting = _greetingService.GetGreetingById(id);

            if (greeting == null)
            {
                return NotFound(new { Success = false, Message = "Greeting not found" });
            }

            return Ok(new { Success = true, Data = greeting });
        }


        [HttpGet]
        [Route("get-greetingsinlist")]
        public IActionResult GetGreetingsInList()
        {
            ResponseBody<List<GreetingEntity>> responseBody = new ResponseBody<List<GreetingEntity>>();

            try
            {
                // Fetch all greetings from the business layer
                var greetings = _greetingService.GetAllGreetingsInList();

                if (greetings == null || !greetings.Any())
                {
                    return NotFound(new { Success = false, Message = "No greetings found" });
                }

                responseBody.Success = true;
                responseBody.Message = "Greetings fetched successfully";
                responseBody.Data = greetings;
            }
            catch (Exception ex)
            {
                responseBody.Success = false;
                responseBody.Message = $"Error: {ex.Message}";
            }

            return Ok(responseBody);
        }
    }
}
