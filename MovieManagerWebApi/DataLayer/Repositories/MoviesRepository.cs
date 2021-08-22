using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public Movie GetMovieWithLoadedData(int id)
        {
            return context.Movies.Include(m => m.Ratings)
                                    .ThenInclude(r => r.User)
                                 .Include(m => m.Actors)
                                 .SingleOrDefault(m => m.Id == id);
        }

        private IEnumerable<Movie> GetTopRated(int pageNumber = 0, int itemsPerPage = 10)
        {
            var ids = context.Ratings.GroupBy(r => r.MovieId)
                                  .Select(g => new { MovieId = g.Key, AverageRating = g.Average(r => r.Value) })
                                  .OrderByDescending(g => g.AverageRating)
                                  .Skip(pageNumber * itemsPerPage)
                                  .Take(itemsPerPage);

            return ids.ToList().Select(id => context.Movies
                               .Include(m => m.Ratings)
                               .Single(m => m.Id == id.MovieId));
        }

        public IEnumerable<Movie> SearchTopRated(string token, int pageNumber = 0, int itemsPerPage = 10)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return GetTopRated(pageNumber, itemsPerPage);
            }

            var query = context.Movies.AsQueryable();

            //Search engine should also understand phrases like "5 stars", "at least 3 stars", "after 2015", "older than 5 years"
            const string specificStarsPattern = "^([1-5]) stars$";
            const string atLeastStarsPattern = "^at least ([1-5]) star(s)?$";
            const string afterYearPattern = @"^after (\d{4})$";
            const string olderThanYears = @"^older than (\d+) years$";

            var trimmedToken = token.Trim();
            if (Regex.IsMatch(trimmedToken, specificStarsPattern))
            {
                var regexResult = Regex.Match(trimmedToken, specificStarsPattern);
                var stars = int.Parse(regexResult.Groups[1].Value);

                //return query.Where(m => m.Ratings.Average(r => r.Value) > stars - 0.5 && m.Ratings.Average(r => r.Value) <= stars + 0.5);

                var matchMovieIds = context.Ratings.GroupBy(r => r.MovieId)
                                                   .Select(g => new { MovieId = g.Key, AverageRating = g.Average(r => r.Value) })
                                                   .Where(a => a.AverageRating > stars - 0.5 && a.AverageRating <= stars + 0.5)
                                                   .OrderByDescending(a => a.AverageRating)
                                                   .Skip(pageNumber * itemsPerPage)
                                                   .Take(itemsPerPage);

                return matchMovieIds.ToList().Select(id => context.Movies
                               .Include(m => m.Ratings)
                               .Single(m => m.Id == id.MovieId));
            }
            else if (Regex.IsMatch(trimmedToken, atLeastStarsPattern))
            {
                var regexResult = Regex.Match(trimmedToken, atLeastStarsPattern);
                var stars = int.Parse(regexResult.Groups[1].Value);

                query = query.Include(m => m.Ratings)
                            .Where(m => m.Ratings.Average(r => r.Value) >= stars)
                            .OrderByDescending(m => m.Ratings.Average(r => r.Value));
            }
            else if (Regex.IsMatch(trimmedToken, afterYearPattern))
            {
                var regexResult = Regex.Match(trimmedToken, afterYearPattern);
                var year = int.Parse(regexResult.Groups[1].Value);

                var afterDate = new DateTime(year, 12, 31);

                query = query.Include(m => m.Ratings)
                            .Where(m => m.ReleaseDate > afterDate)
                            .OrderByDescending(m => m.Ratings.Average(r => r.Value));

            }
            else if (Regex.IsMatch(trimmedToken, olderThanYears))
            {
                var regexResult = Regex.Match(trimmedToken, olderThanYears);
                var years = int.Parse(regexResult.Groups[1].Value);

                var beforeDate = DateTime.Now.AddYears(-years);
                query = query.Include(m => m.Ratings).Where(m => m.ReleaseDate < beforeDate)
                            .OrderByDescending(m => m.Ratings.Average(r => r.Value));
            }
            else
            {
                query = query.Include(m => m.Ratings)
                            .Where(m => EF.Functions.ILike(m.Title, $"%{token}%") || EF.Functions.ILike(m.Description, $"%{token}%"))
                            .OrderByDescending(m => m.Ratings.Average(r => r.Value));
            }

            return query.Skip(pageNumber * itemsPerPage).Take(itemsPerPage);
        }
    }
}
