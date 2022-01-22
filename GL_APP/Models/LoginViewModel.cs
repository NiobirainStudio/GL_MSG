using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GL_APP.Models
{
    public class LoginViewModel
    {
        [Required]
        //[Remote("UserDoesntExist", "Home")]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
