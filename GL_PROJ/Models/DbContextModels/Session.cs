using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models.DbContextModels
{
    public class Sessions
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        [MaxLength(64)]
        public string Session { get; set; }

    }
}
