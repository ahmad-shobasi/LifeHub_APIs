using LifeHub_APIs.Models;
using LifeHub_APIs.Models.DailyTasks;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<DailyTask> Tasks => Set<DailyTask>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DailyTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
