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
        public DbSet<TVShow> TVShows { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Actor> Actors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("MoviesDatabase");
            optionsBuilder.UseNpgsql(connectionString);
        }

        public static void LogStuff(string value)
        {
            const string logPath = @"C:\Users\Luka\Desktop\movieLog.txt";
            File.AppendAllText(logPath, "\n=================================================\n}" + value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                   .HasIndex(u => u.Email)
                   .IsUnique();

            modelBuilder.Entity<User>()
                   .HasIndex(u => u.Username)
                   .IsUnique();

            modelBuilder.Entity<Rating>().HasIndex("UserId", "MovieId").IsUnique();
            modelBuilder.Entity<Rating>().HasIndex("UserId", "TVShowId").IsUnique();

            // movie actors many to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Actors)
                        .WithMany(d => d.StarredInMovies)
                        .UsingEntity(j => j.ToTable("ActorMovie"));

            modelBuilder.Entity<TVShow>()
                        .HasMany(m => m.Actors)
                        .WithMany(d => d.StarredInTvShows)
                        .UsingEntity(j => j.ToTable("ActorTVShow"));

            // movie has many reviews
            modelBuilder.Entity<Movie>().HasMany(m => m.Ratings);

            // tvshow has many reviews
            modelBuilder.Entity<TVShow>().HasMany(m => m.Ratings);

            // user has many reviews
            modelBuilder.Entity<User>().HasMany(m => m.Ratings);
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
