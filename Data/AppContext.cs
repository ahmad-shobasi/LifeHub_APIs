using LifeHub_APIs.Models;
using LifeHub_APIs.Models.CalendarEvents;
using LifeHub_APIs.Models.DailyTasks;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<DailyTask> Tasks => Set<DailyTask>();
        public DbSet<CalendarEvent> CalendarEvents => Set<CalendarEvent>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DailyTask>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DailyTask>()
                .Property(t => t.priority)
                .HasConversion<string>();

            modelBuilder.Entity<CalendarEvent>()
                .HasOne(e => e.User)
                .WithMany(u=> u.Events)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
        

    }
}
