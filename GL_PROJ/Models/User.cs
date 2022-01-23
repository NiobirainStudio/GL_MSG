using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models
{
    // This class defines the user model
    public class User
    {
        // Custom user Id
        public int Id { get; set; }

        // User name
        [Required]
        public string Name { get; set; }

        // User password
        [Required]
        public string Password { get; set; }
    }
}
