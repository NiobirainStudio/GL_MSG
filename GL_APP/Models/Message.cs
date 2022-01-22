using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GL_APP.Models
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public DateTime When { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }
        
        [NotMapped]
        public string UserName { get; set; }
    }
}
