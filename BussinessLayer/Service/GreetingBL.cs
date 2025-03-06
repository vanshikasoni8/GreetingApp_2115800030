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
            try
            {
                return "Hello, World!";
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting the greeting message.", ex);
            }
            
        }


        public string NameGreeting(string firstName, string lastName)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("An error occurred while generating the name greeting.", ex);
            }
        }


        public void SaveGreetingMessage(GreetingEntity greetingEntity)
        {
            try
            {
                _greetingRL.SaveGreeting(greetingEntity);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the greeting message.", ex);
            }
        }

        public List<GreetingEntity> GetSavedGreetings()
        {
            try
            {
                return _greetingRL.GetAllGreetings();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching saved greetings.", ex);
            }
        }

        public GreetingEntity GetGreetingById(int id)
        {
            try
            {
                return _greetingRL.GetGreetingById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the greeting with ID {id}.", ex);
            }
        }


        public List<GreetingEntity> GetAllGreetings()
        {
            try
            {
                return _greetingRL.GetAllGreetings();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all greetings.", ex);
            }
        }

        public List<GreetingEntity> GetAllGreetingsInList()
        {
            return _greetingRL.GetAllGreetings();
        }

        public bool UpdateGreeting(int id, GreetingEntity updatedGreeting)
        {
            try
            {
                return _greetingRL.UpdateGreeting(id, updatedGreeting);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while updating the greeting with ID {id}.", ex);
            }
        }

        public bool DeleteGreeting(int id)
        {
            try
            {
                return _greetingRL.DeleteGreeting(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the greeting with ID {id}.", ex);
            }
        }
    }
}
