using System;
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

        // Add the method to list all greetings
        List<GreetingEntity> GetSavedGreetings();

        // Add the method to edit a greeting
        bool UpdateGreeting(int id, GreetingEntity updatedGreeting);

    }
}
