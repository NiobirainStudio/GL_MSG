using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GL_PROJ.Models.DbContextModels
{
    // This class defines the user model
    public class User
    {
        // Custom user Id
        public int Id { get; set; }

        // User nickname
        [Required]
        [MaxLength(32)]
        public string NickName { get; set; }

        [Required]
        [MaxLength(32)]
        public string AccountName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(320)]
        public string Email { get; set; }

        [Required]
        [ForeignKey("UserIcon")]
        public int UserIcon { get; set; }
        
        [Required]
        public Icons Icon { get; set; }

        // User password
        [Required]
        [MaxLength(32)]
        public string Password { get; set; }

        // User description
        [MaxLength(128)]
        public string Description { get; set; }
    }
}