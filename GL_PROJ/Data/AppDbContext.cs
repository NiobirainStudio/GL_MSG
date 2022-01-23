using GL_PROJ.Models;
using Microsoft.EntityFrameworkCore;

namespace GL_PROJ.Data
{
    // This class is defines as appdbcontext
    // The goal of this class is to build a database based on its contents
    public class AppDbContext : DbContext
    {
        // Messages table
        public DbSet<Message> Messages { get; set; }
        
        // Users table
        public DbSet<User> Users { get; set; }

        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
