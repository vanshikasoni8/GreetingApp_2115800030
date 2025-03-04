using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interface;
using Microsoft.Extensions.Logging;
using NLog;

namespace BussinessLayer.Service
{
    public class GreetingServiceBL : IGreetingBL
    {
        private readonly ILogger<GreetingServiceBL> _logger;

        public GreetingServiceBL(ILogger<GreetingServiceBL> logger)
        {
            _logger = logger;
        }

        public string GetGreeting()
        {
            _logger.LogInformation("GreetingService: Returning 'Hello World' message.");
            return "Hello World";
        }
    }
}
