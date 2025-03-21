using System.ComponentModel.DataAnnotations;
using RepositaryLayer.Service;



namespace RepositaryLayer.Entity
{
    public class UserEntity 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = " ";

        [Required]
        public string Email { get; set; } = " ";
        [Required]

        //hasing and salting
        public string PasswordHash { get; set; }
        public string Salt { get; internal set; }

        public DateTime? ResetTokenExpiry { get; set; }
        public string ResetToken { get; set; }

        public ICollection<GreetingEntity> Greeting { get; set; }
    }
}

