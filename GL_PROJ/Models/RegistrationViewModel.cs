using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models
{
    // This class defines the registration model
    public class RegistrationViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
