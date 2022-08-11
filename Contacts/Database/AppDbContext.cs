
using Contacts.Models;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Contact>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasMany(u => u.Contacts).WithOne(t => t.User);
        }
    }
}
