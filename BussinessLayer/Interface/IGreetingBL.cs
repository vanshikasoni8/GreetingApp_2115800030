using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositaryLayer.Entity;

namespace BussinessLayer.Interface
{
    public interface IGreetingBL
    {
        string GetGreetingMessage();

        string NameGreeting(string firstName, string lastName);

        void SaveGreetingMessage(GreetingEntity greeting);
        List<GreetingEntity> GetSavedGreetings();

        GreetingEntity GetGreetingById(int id);

        
        List<GreetingEntity> GetAllGreetingsInList();

        // Add the method to update a greeting
        bool UpdateGreeting(int id, GreetingEntity updatedGreeting);
    }
}
