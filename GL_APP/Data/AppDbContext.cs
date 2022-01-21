using GL_APP.Models;
using Microsoft.EntityFrameworkCore;

namespace GL_APP.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
