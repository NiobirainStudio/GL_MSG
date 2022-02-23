using System.ComponentModel.DataAnnotations;

namespace GL_PROJ.Models.DbContextModels
{
    public class File
    {
        [Required]
        public int FileId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
