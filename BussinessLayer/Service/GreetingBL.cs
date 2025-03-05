using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLayer.Interface;
using RepositaryLayer.Entity;
using RepositaryLayer.Interface;

namespace BussinessLayer.Service
{
    public class GreetingBL: IGreetingBL
    {
        private readonly IGreetingRL _greetingRL;
        

        public GreetingBL(IGreetingRL greetingRL)
        {
            _greetingRL = greetingRL;
        }

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


        public void SaveGreetingMessage(GreetingEntity greetingEntity)
        {
            _greetingRL.SaveGreeting(greetingEntity);
        }

        public List<GreetingEntity> GetSavedGreetings()
        {
            return _greetingRL.GetAllGreetings();
        }

        public GreetingEntity GetGreetingById(int id)
        {
            // Call the repository method to get the greeting by ID
            return _greetingRL.GetGreetingById(id);
        }


        public List<GreetingEntity> GetAllGreetings()
        {
            return _greetingRL.GetAllGreetings();
        }

        public List<GreetingEntity> GetAllGreetingsInList()
        {
            return _greetingRL.GetAllGreetings();
        }
    }
}
