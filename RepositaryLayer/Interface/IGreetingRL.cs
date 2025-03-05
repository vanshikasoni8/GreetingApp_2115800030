﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositaryLayer.Entity;
using RepositaryLayer.Service;

namespace RepositaryLayer.Interface
{
    public interface IGreetingRL
    {
        //void SaveGreeting(GreetingEntity greeting);
        List<GreetingEntity> GetAllGreetings();

        GreetingEntity SaveGreeting(GreetingEntity greeting);

        GreetingEntity GetGreetingById(int id);

    }
}
