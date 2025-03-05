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
            //_logger.LogInformation("Saving greeting: {GreetingMessage}", greeting.Message);  // ✅ Log the data
            _context.Greetings.Add(greeting);
            _context.SaveChanges();
            return greeting;
        }

        public List<GreetingEntity> GetAllGreetings()  // Implement this method
        {
            return _context.Greetings.ToList();  // Fetch all greetings from the database
        }
        public List<GreetingEntity> GetSavedGreetings()
        {
            return _context.Greetings.ToList();
        }

        public GreetingEntity GetGreetingById(int id)
        {
            // Find a greeting by its ID
            return _context.Greetings.FirstOrDefault(g => g.Id == id);
        }

        public bool UpdateGreeting(int id, GreetingEntity updatedGreeting)
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


        public bool DeleteGreeting(int id)
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

    }
}
