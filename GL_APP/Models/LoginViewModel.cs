using System.ComponentModel.DataAnnotations;

namespace GL_APP.Models
{
    public class LoginViewModel
    {
        //[Display(Name = "Enter your username")]
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
