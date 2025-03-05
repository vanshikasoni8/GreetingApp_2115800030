using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modellayer.Context;
using Modellayer.Model;
using RepositaryLayer.Entity;
using RepositaryLayer.Interface;

namespace RepositaryLayer.Service
{
    public class GreetingRL : IGreetingRL
    {
        private readonly AppDbContext _context;

        public GreetingRL(AppDbContext context)
        {
            _context = context;
        }

        public void SaveGreeting(GreetingEntity greeting)
        {
            _context.Greetings.Add(new GreetingMessage
            {
                Id = greeting.Id,
                Message = greeting.Message
            });
            _context.SaveChanges();
        }

        public List<GreetingEntity> GetAllGreetings()
        {
            return _context.Greetings.Select(g => new GreetingEntity
            {
                Id = g.Id,
                Message = g.Message
            }).ToList();
        }
    }
}
