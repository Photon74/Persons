using Microsoft.EntityFrameworkCore;

using Persons.DAL.Entities;

namespace Persons.DAL
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=GB;Username=postgres;Password=3299;");
            optionsBuilder.LogTo(message => System.Diagnostics.Debug.WriteLine(message));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>();
        }
    }
}
