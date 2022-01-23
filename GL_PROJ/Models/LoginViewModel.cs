using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models
{
    // This class defines the login model
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
