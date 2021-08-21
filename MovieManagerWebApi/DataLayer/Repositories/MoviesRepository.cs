using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
    {
        public MoviesRepository(MovieDbContext context) : base(context)
        {
        }

        public IEnumerable<Movie> GetTopRatedMovies(int count = 10)
        {
            return context.Reviews.GroupBy(r => r.MovieId)
                                  .Select(g => new { MovieId = g.Key, AverageRating = g.Average(m => m.Rating) })
                                  .OrderByDescending(g => g.AverageRating)
                                  .Take(count)
                                  .Select(c => context.Movies.Find(c.MovieId));
        }

        public IEnumerable<Movie> GetPage(string token, string orderBy, bool ascending, int pageNumber = 0, int pageCount = 10)
        {
            var query = context.Movies.AsQueryable();

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.Where(m => EF.Functions.Like(m.Title, $"%{token}%") || EF.Functions.Like(m.Description, $"%{token}%"));
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                if (string.Equals(orderBy, "title", StringComparison.OrdinalIgnoreCase))
                {
                    query = ascending
                        ? query.OrderBy(m => m.Title)
                        : query.OrderByDescending(m => m.Title);
                }
                else if (string.Equals(orderBy, "release", StringComparison.OrdinalIgnoreCase))
                {
                    query = ascending
                        ? query.OrderBy(m => m.ReleaseYear)
                        : query.OrderByDescending(m => m.ReleaseYear);
                }
                else if (string.Equals(orderBy, "duration", StringComparison.OrdinalIgnoreCase))
                {
                    query = ascending
                        ? query.OrderBy(m => m.Runtime)
                        : query.OrderByDescending(m => m.Runtime);
                }
                else
                {
                    throw new ArgumentException($"Invalid order by value '{orderBy}'. Expecting 'title', 'release' or 'duration'.");
                }
            }

            return query.Skip(pageNumber * pageCount).Take(pageCount);
        }
    }
}
