using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models
{
    // This class defines the message model
    public class Message
    {
        // Custom message Id
        public int Id { get; set; }

        // Message text
        [Required]
        public string Text { get; set; }

        // Date the message was sent 
        public DateTime When { get; set; }

        // Message author Id
        public int UserId { get; set; }

        // Defining a foreign key from users
        public User User { get; set; }
    }
}
