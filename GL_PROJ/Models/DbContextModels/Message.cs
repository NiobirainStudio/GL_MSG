using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models.DbContextModels
{
    // This class defines the message model
    public class Message
    {
        // Custom message Id
        public int Id { get; set; }

        // Message text
        [Required]
        public string Data { get; set; }

        // Date the message was sent 
        [Required]
        public DateTime Date { get; set; }

        // Message type
        [Required]
        public int Type { get; set; }


        // Message author Id
        [Required]
        public int UserId { get; set; }

        // Defining a foreign key from users
        [Required]
        public User User { get; set; }


        // Group Id
        [Required]
        public int GroupId { get; set; }

        // Defining a foreign key from groups
        [Required]
        public Group Group { get; set; }
    }
}
