using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositaryLayer.Entity
{
    public class GreetingEntity
    {

        
            [Key]
            public int Id { get; set; }
        [Required]
            public string FirstName { get; set; } = " ";

        [Required]
            public string LastName { get; set; } = " ";

        [Required]
        public string Message { get; set; }

        // Foreign Key Property
        public int UserId { get; set; }

        // Navigation Property (Reference to User)
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }

    }
}
