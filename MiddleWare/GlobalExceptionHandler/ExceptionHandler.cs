using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MiddleWare.GlobalExceptionHandler
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context); // Continue the pipeline
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception caught: {ex.Message}");

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new
                {
                    success = false,
                    message = ex.Message
                };

                var jsonResponse = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
