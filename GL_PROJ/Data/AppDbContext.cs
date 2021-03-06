using GL_PROJ.Models.DbContextModels;
using Microsoft.EntityFrameworkCore;

namespace GL_PROJ.Data
{
    // This class is defines as appdbcontext
    // The goal of this class is to build a database based on its contents
    public class AppDbContext : DbContext
    {
        // Users table
        public DbSet<User> Users { get; set; }


        // Groups table
        public DbSet<Group> Groups { get; set; }


        // Messages table
        public DbSet<Message> Messages { get; set; }


        // User - Group relation
        public DbSet<UserGroupRelation> UserGroupRelations { get; set; }



        // Constructor
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // User - Group relation primary key relabeling
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserGroupRelation>()
                .HasKey(pc => new { pc.UserId, pc.GroupId });
        }
    }
}
