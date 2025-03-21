using System.ComponentModel.DataAnnotations;



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

    }
}

