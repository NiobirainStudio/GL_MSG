using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models.DTO
{
    public class MessageDTO
    {
        // Custom message Id
        public int MessageId { get; set; }

        // Message text
        public string Data { get; set; }

        // Date the message was sent 
        public DateTime Date { get; set; }

        // Message type
        public int Type { get; set; }

        // Message author Id
        public int UserId { get; set; }

        // Author Name
        public string UserName { get; set; }

        // Group Id
        public int GroupId { get; set; }

    }
}
