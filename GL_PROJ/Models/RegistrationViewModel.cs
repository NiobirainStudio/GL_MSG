using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models
{
    // This class defines the registration model
    public class RegistrationViewModel
    {
        // Required username
        [Required]
        public string UserName { get; set; }

        // Required user password
        [Required]
        public string Password { get; set; }
    }
}
