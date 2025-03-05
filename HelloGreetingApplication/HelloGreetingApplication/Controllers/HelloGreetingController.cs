using BussinessLayer.Interface;
using BussinessLayer.Service;
using Microsoft.AspNetCore.Mvc;
using Model_Layer.Model;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// class providing APi for hellogreeting 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase

    {
        private readonly ILogger<HelloGreetingController> _logger;
        //private readonly GreetingBL _greetingService;
        private readonly IGreetingBL _greetingBL;

        /// <summary>
        /// Constructor to initialize logger.
        /// </summary>
        public HelloGreetingController(ILogger<HelloGreetingController> logger, IGreetingBL greetingBL)
        {
            _logger = logger;
            //_greetingService = greetingService;
            _greetingBL = greetingBL;
        }

        /// <summary>
        /// Get Method to get the greeting message
        /// </summary>
        /// <returns> Hello World!</returns>
        [HttpGet]
        public IActionResult Get()
        {

            _logger.LogInformation("GET request received.");
            ResponseModel<string> responseModel = new ResponseModel<string>();
            responseModel.Success = true;
            responseModel.Message = " Hello to Greeting App API EndPoint";
            responseModel.Data = "Hello World!";
            return Ok(responseModel);
        }



        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {

            _logger.LogInformation("POST request received with Key: {Key}, Value: {Value}", requestModel.Key, requestModel.Value);

            ResponseModel<string> responseModel = new ResponseModel<string>();


            responseModel.Success = true;
            responseModel.Message = "Received successfully";
            responseModel.Data = $"key :{requestModel.Key},Value:{requestModel.Value}";
            return Ok(responseModel);
        }



        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {

            _logger.LogInformation("PUT request received with Key: {Key}, Value: {Value}", requestModel.Key, requestModel.Value);

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Updated successfully",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            return Ok(responseModel);
        }

        [HttpPatch]
        public IActionResult Patch(RequestModel requestModel)
        {
            _logger.LogInformation("PATCH request received with Key: {Key}, Value: {Value}", requestModel.Key, requestModel.Value);
            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Partially updated successfully",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            return Ok(responseModel);
        }

        [HttpDelete]
        public IActionResult Delete(RequestModel requestModel)
        {
            _logger.LogInformation("DELETE request received for Key: {Key}", requestModel.Key);

            ResponseModel<string> responseModel = new ResponseModel<string>
            {
                Success = true,
                Message = "Deleted successfully",
                Data = $"Key: {requestModel.Key}, Value: {requestModel.Value}"
            };

            return Ok(responseModel);
        }

        /// <summary>
        /// for greeting UC2
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("greeting")]
        public IActionResult Greetings([FromQuery] string? firstName, [FromQuery] string? lastName)
        {
            ResponseModel<string> ResponseModel = new ResponseModel<string>();

            ResponseModel.Success = true;
            ResponseModel.Message = "Greeting message fetched successfully";
            ResponseModel.Data = _greetingBL.GetGreeting(firstName, lastName);

            return Ok(ResponseModel);
        }


        [HttpPost("save")]
        public IActionResult SaveGreeting([FromBody] GreetingDTO greetingDTO)
        {
            var result = _greetingBL.SaveGreeting(
                greetingDTO.Key,
                greetingDTO.Value,
                greetingDTO.FirstName,
                greetingDTO.LastName,
                greetingDTO.Message
            );

            return Ok(new { message = "Greeting saved successfully!", data = result });
        }

    }
}
