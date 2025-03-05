using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositaryLayer.Entity
{
    public class GreetingEntity
    {
        
            [Key]
            public int Id { get; set; }
            public string FirstName { get; set; } = " ";

            public string LastName { get; set; } = " ";

            public string Message { get; set; } = " ";
        [Required]
        public string Email { get; set; }
        
    }
}
