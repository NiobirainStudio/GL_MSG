using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GL_PROJ.Models.DbContextModels
{
    // This class defines the user model
    public class Group
    {
        // Custom group Id
        public int Id { get; set; }

        // Group name
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        
        [Required]

        [ForeignKey("GroupIcon")]
        public int GroupIcon { get; set; }
        
        [Required]
        public Icons Icon { get; set; }
        
        [Required]
        public int  GroupType { get; set; }

        // Group description
        public string Description { get; set; }
    }
}