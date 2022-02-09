using GL_PROJ.Models.DbContextModels;

namespace GL_PROJ.Models
{
    // This class defines the main model
    public partial class MainViewModel
    {
        public int UserId { get; set; }

        // The current model is designed to display all messages from the database. 
        public Group[] Groups;
    }
}