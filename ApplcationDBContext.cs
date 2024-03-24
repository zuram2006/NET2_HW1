using Microsoft.EntityFrameworkCore;
using Reddit.Models;

namespace Reddit
{
    public class ApplcationDBContext: DbContext
    {
        public ApplcationDBContext(DbContextOptions<ApplcationDBContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Community> Communities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Community entity
            modelBuilder.Entity<Community>(entity =>
            {
                // Relationship between Community and User (Owner)
                entity.HasOne(c => c.Owner)
                      .WithMany(u => u.OwnedCommunities)
                      .HasForeignKey(e => e.OwnerId)
                      .OnDelete(DeleteBehavior.SetNull); // Assuming restriction on delete

        /*        // Relationship between Community and Posts
                entity.HasMany(e => e.Posts)
                      .WithOne(p => p.Community) // Assuming there's a Community navigation property in Post
                      .HasForeignKey(p => p.CommunityId); // Assuming there's a CommunityId foreign key in Post*/

                // Many-to-many relationship between Community and User (Subscribers)
                entity.HasMany(c => c.Subscribers)
                      .WithMany(u => u.SubscribedCommunities)
                      .UsingEntity(j => j.ToTable("CommunitySubscriptions")); // Intermediate table for many-to-many
            });

/*            // Configure the User entity
            modelBuilder.Entity<User>(entity =>
            {
          
                // Relationship between User and Post
                entity.HasMany(e => e.Posts)
                      .WithOne(p => p.Author) // Assuming there's a User navigation property in Post
                      .HasForeignKey(p => p.AuthorId); // Assuming there's a UserId foreign key in Post

            });*/

            base.OnModelCreating(modelBuilder);
        }
    }
}
