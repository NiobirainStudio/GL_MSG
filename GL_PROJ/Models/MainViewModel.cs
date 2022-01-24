using GL_PROJ.Models.DbContextModels;

namespace GL_PROJ.Models
{
    // This class defines the main model
    public partial class MainViewModel
    {
        // The current model is designed to display all groups from the database. 
        public List<Group> Groups { get; set; }
    }
}