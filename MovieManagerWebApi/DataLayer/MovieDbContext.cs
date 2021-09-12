using System;
using System.IO;
using System.Linq;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataLayer
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext()
        {

        }

        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(LogToLocalFile);
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("MoviesDatabasePostgre");
            optionsBuilder.UseNpgsql(connectionString);
        }

        public static void LogToLocalFile(string text)
        {
            string logPath = @"C:\Users\luka.budrak\Desktop\log.txt";
            if (File.Exists(logPath))
            {
                File.AppendAllText(logPath, "\n=================================================\n}" + text);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasIndex(u => u.Email)
                   .IsUnique();

            modelBuilder.Entity<User>()
                   .HasIndex(u => u.Username)
                   .IsUnique();

            modelBuilder.Entity<Review>().HasIndex("UserId", "MovieId").IsUnique();

            // movie actors many to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Actors)
                        .WithMany(d => d.StarredInMovies)
                        .UsingEntity(j => j.ToTable("ActorMovie"));

            // movie has many reviews
            modelBuilder.Entity<Movie>().HasMany(m => m.Reviews);

            // user has many reviews
            modelBuilder.Entity<User>().HasMany(m => m.Reviews);
        }

        public override int SaveChanges()
        {
            // get all modified or added entities
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            // set modified/created timestamps
            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).ModifiedWhen = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedWhen = DateTime.Now;
                }
            }

            // do default functionality
            return base.SaveChanges();
        }
    }
}
