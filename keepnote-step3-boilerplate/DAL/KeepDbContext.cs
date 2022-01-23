using Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class KeepDbContext:DbContext
    {
        public KeepDbContext() { }
        public KeepDbContext(DbContextOptions<KeepDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
    }
}
