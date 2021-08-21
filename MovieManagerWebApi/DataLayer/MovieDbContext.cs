using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Npgsql.EntityFrameworkCore;

namespace DataLayer
{
    internal class MovieDbContext : DbContext
    {
        public MovieDbContext() : base()
        {

        }

        public MovieDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var connectionString = configuration.GetConnectionString("MoviesDatabase");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // genre movie many to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Genres)
                        .WithMany(g => g.Movies);

            // movie directors many to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Directors)
                        .WithMany(d => d.Directed)
                        .UsingEntity(j => j.ToTable("DirectorMovie"));

            // movie actors many to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Actors)
                        .WithMany(d => d.StarredIn)
                        .UsingEntity(j => j.ToTable("ActorMovie"));

            // movie writers many to many
            modelBuilder.Entity<Movie>()
                        .HasMany(m => m.Writers)
                        .WithMany(d => d.Written)
                        .UsingEntity(j => j.ToTable("WriterMovie"));

            // movie has many reviews
            modelBuilder.Entity<Movie>().HasMany(m => m.Reviews);

            // user has watched many movies
            modelBuilder.Entity<User>().HasMany(m => m.ToWatch);

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
