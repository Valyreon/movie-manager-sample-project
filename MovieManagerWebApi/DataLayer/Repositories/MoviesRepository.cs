using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
    {
        public MoviesRepository(MovieDbContext context) : base(context)
        {
        }

        public IEnumerable<Movie> GetTopRated(int count = 10)
        {
            var ids = context.Ratings.GroupBy(r => r.MovieId)
                                  .Select(g => new { MovieId = g.Key, AverageRating = g.Average(r => r.Value) })
                                  .OrderByDescending(g => g.AverageRating)
                                  .Take(count);

            return ids.ToList().Select(id => context.Movies.Find(id.MovieId));
        }

        public IEnumerable<Movie> Search(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return GetTopRated();
            }

            var query = context.Movies.AsQueryable();

            //Search engine should also understand phrases like "5 stars", "at least 3 stars", "after 2015", "older than 5 years"
            const string specificStarsPattern = "([1-5]) stars";
            const string atLeastStarsPattern = "at least ([1-5]) stars";
            const string afterYearPattern = @"after (\d{4})";
            const string olderThanYears = @"older than (\d+) years";

            var trimmedToken = token.Trim();
            if (Regex.IsMatch(trimmedToken, specificStarsPattern))
            {
                var regexResult = Regex.Match(trimmedToken, specificStarsPattern);
                var stars = int.Parse(regexResult.Groups[1].Value);

                query = query.Where(m => m.Ratings.Average(r => r.Value) >= stars);
            }
            else if (Regex.IsMatch(trimmedToken, atLeastStarsPattern))
            {

            }
            else if (Regex.IsMatch(trimmedToken, afterYearPattern))
            {

            }
            else if (Regex.IsMatch(trimmedToken, olderThanYears))
            {

            }
            else
            {

            }

            return query;
        }
    }
}
