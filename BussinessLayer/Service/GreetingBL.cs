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
    }
}
