using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models.DbContextModels
{
    // This class defines the user model
    public class Group
    {
        // Custom group Id
        public int Id { get; set; }

        // Group name
        [Required]
        public string Name { get; set; }

        // Group description
        public string Description { get; set; }
    }
}