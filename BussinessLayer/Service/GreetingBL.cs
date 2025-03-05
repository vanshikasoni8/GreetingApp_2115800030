﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interface;

namespace BussinessLayer.Service
{
    public class GreetingBL: IGreetingBL
    {
        public string GetGreetingMessage()
        {
            return "Hello, World!";
        }

        public string NameGreeting(string firstName, string lastName)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName))
            {
                return $"Hello {firstName} {lastName}!";
            }
            else if (!string.IsNullOrWhiteSpace(firstName))
            {
                return $"Hello {firstName}!";
            }
            else if (!string.IsNullOrWhiteSpace(lastName))
            {
                return $"Hello {lastName}!";
            }
            else
            {
                return "Hello World!";
            }
        }

    }
}
