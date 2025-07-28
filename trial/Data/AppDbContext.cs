using Microsoft.EntityFrameworkCore;

using trial.Models;

namespace trial.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Trips> Trips { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<DBinterest> DBinterests { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                 .HasMany(c => c.trips)
                 .WithOne(t => t.Country)
                 .HasForeignKey(t => t.CountryId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trips>()
               .HasMany(t => t.Comments)
               .WithOne(c => c.Trip)
               .HasForeignKey(c => c.TripId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Person)
                .HasForeignKey(c => c.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.DBinterests)
                .WithOne(d => d.Person)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Trips>()
                .HasMany(t => t.Books)
                .WithOne(b => b.Trip)
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Person)
                .HasForeignKey(b => b.PersonId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
