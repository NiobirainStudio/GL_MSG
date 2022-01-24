using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models
{
    // This class defines the login model
    public class LoginViewModel
    {
        // Required username
        [Required]
        public string UserName { get; set; }

        // Required user password
        [Required]
        public string Password { get; set; }
    }
}
