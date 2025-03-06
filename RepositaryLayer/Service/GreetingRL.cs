using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modellayer.Context;
using Modellayer.Model;
using RepositaryLayer.Entity;
using RepositaryLayer.Interface;

namespace RepositaryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly AppDbContext _context;

        public GreetingRL(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public GreetingEntity SaveGreeting(GreetingEntity greeting)
        {
            try
            {
                _context.Greetings.Add(greeting);
                _context.SaveChanges();
                return greeting;
            }
            catch (Exception ex)
            {
                // Log the exception if needed (e.g., using a logger)
                throw new Exception("An error occurred while saving the greeting message.", ex);
            }
        }

        public List<GreetingEntity> GetAllGreetings()  // Implement this method
        {
            try
            {
                return _context.Greetings.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all greetings.", ex);
            }
        }
        public List<GreetingEntity> GetSavedGreetings()
        {
            try
            {
                return _context.Greetings.ToList();
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
                return _context.Greetings.FirstOrDefault(g => g.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching the greeting with ID {id}.", ex);
            }
        }

        public bool UpdateGreeting(int id, GreetingEntity updatedGreeting)
        {
            try
            {
                var existingGreeting = _context.Greetings.Find(id);
                if (existingGreeting == null)
                {
                    return false; // Greeting not found
                }

                // Update the greeting properties
                existingGreeting.Message = updatedGreeting.Message;

                // Save changes to the database
                _context.SaveChanges();
                return true;
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
                var existingGreeting = _context.Greetings.Find(id);
                if (existingGreeting == null)
                {
                    return false; // Greeting not found
                }

                // Remove the greeting
                _context.Greetings.Remove(existingGreeting);

                // Save changes
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting the greeting with ID {id}.", ex);
            }
        }

    }
}
