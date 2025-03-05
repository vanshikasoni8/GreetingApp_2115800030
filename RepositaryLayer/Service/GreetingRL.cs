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

    }
}
