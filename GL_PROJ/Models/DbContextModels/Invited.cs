using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models.DbContextModels
{
    public class Invited
    {
        // User Id
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

        [Required]
        public int InviterId { get; set; }

    }
}
