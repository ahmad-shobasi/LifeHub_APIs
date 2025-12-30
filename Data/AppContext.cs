using LifeHub_APIs.Models;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
    }
}
