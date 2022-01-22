using System.ComponentModel.DataAnnotations;

namespace GL_APP.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
