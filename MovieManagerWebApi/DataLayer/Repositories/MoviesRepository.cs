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

        public (IEnumerable<Movie> PageItems, int TotalNumberOfPages) SearchTopRated(string token = null, int pageNumber = 0, int itemsPerPage = 10)
        {
            var query = context.Movies.AsQueryable();

            var specificStarsRegex = new Regex("^([1-5]) stars$");
            var atLeastStarsRegex = new Regex("^at least ([1-5]) star(s)?$");
            var afterYearRegex = new Regex(@"^after (\d{4})$");
            var olderThanRegex = new Regex(@"^older than (\d+) years$");
            Match match = null;

            var trimmedToken = token == null ? "" : token.Trim();
            if ((match = specificStarsRegex.Match(trimmedToken)).Success)
            {
                var stars = int.Parse(match.Groups[1].Value);

                query = query.Where(m => m.Ratings.Average(r => r.Value) > stars - 0.5 && m.Ratings.Average(r => r.Value) <= stars + 0.5);
            }
            else if ((match = atLeastStarsRegex.Match(trimmedToken)).Success)
            {
                var stars = int.Parse(match.Groups[1].Value);

                query = query.Where(m => m.Ratings.Average(r => r.Value) >= stars)
                             .OrderByDescending(m => m.Ratings.Average(r => r.Value));
            }
            else if ((match = afterYearRegex.Match(trimmedToken)).Success)
            {
                var year = int.Parse(match.Groups[1].Value);
                var afterDate = new DateTime(year, 12, 31);

                query = query.Where(m => m.ReleaseDate > afterDate);
            }
            else if ((match = olderThanRegex.Match(trimmedToken)).Success)
            {
                var years = int.Parse(match.Groups[1].Value);
                var beforeDate = DateTime.Now.AddYears(-years);

                query = query.Where(m => m.ReleaseDate < beforeDate);
            }
            else if (!string.IsNullOrWhiteSpace(trimmedToken))
            {
                query = query.Where(m => EF.Functions.ILike(m.Title, $"%{token}%") || EF.Functions.ILike(m.Description, $"%{token}%"));
            }

            var resultItems = query.Include(m => m.Ratings)
                                   .OrderByDescending(m => m.Ratings.Average(r => r.Value))
                                   .Skip(pageNumber * itemsPerPage)
                                   .Take(itemsPerPage);

            return (resultItems, CalculateNumberOfPages(query.Count(), itemsPerPage));
        }

        private int CalculateNumberOfPages(int totalNumber, int pageSize)
        {
            var nbFullyFilledPages = totalNumber / pageSize;
            var nbPartiallyFilledPages = (totalNumber % pageSize == 0) ? 0 : 1;

            return nbFullyFilledPages + nbPartiallyFilledPages;
        }
    }
}
