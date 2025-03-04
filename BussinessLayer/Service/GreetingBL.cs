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

        public string GetGreeting(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return $"Hello, {firstName} {lastName}!";
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                return $"Hello, {firstName}!";
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                return $"Hello, Mr./Ms. {lastName}!";
            }
            else
            {
                return "Hello, World!";
            }
        }
    }
}
