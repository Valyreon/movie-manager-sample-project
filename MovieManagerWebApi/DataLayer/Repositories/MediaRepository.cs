using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public abstract class MediaRepository<T> : GenericRepository<T>, IMediaRepository<T> where T : Multimedia
    {
        public MediaRepository(MovieDbContext context) : base(context)
        {
        }

        public T GetMediaWithLoadedData(int id)
        {
            return context.Set<T>().Include(m => m.Ratings)
                                    .ThenInclude(r => r.User)
                                 .Include(m => m.Actors)
                                 .SingleOrDefault(m => m.Id == id);
        }

        public (IEnumerable<T> PageItems, int TotalNumberOfPages) SearchTopRated(string token = null, int pageNumber = 0, int itemsPerPage = 10)
        {
            var query = context.Set<T>().Include(m => m.Ratings)
                    .Select(m => new { Media = m, AverageRating = (double?)m.Ratings.Average(r => r.Value) });

            var specificStarsRegex = new Regex("^([1-5]) stars$");
            var atLeastStarsRegex = new Regex("^at least ([1-5]) star(s)?$");
            var afterYearRegex = new Regex(@"^after (\d{4})$");
            var olderThanRegex = new Regex(@"^older than (\d+) years$");
            Match match = null;

            var trimmedToken = token == null ? "" : token.Trim();
            if ((match = specificStarsRegex.Match(trimmedToken)).Success)
            {
                var stars = int.Parse(match.Groups[1].Value);

                query = query.Where(a => a.AverageRating > stars - 0.5 && a.AverageRating <= stars + 0.5);
            }
            else if ((match = atLeastStarsRegex.Match(trimmedToken)).Success)
            {
                var stars = int.Parse(match.Groups[1].Value);

                query = query.Where(m => m.AverageRating >= stars)
                             .OrderByDescending(m => m.AverageRating);
            }
            else if ((match = afterYearRegex.Match(trimmedToken)).Success)
            {
                var year = int.Parse(match.Groups[1].Value);
                var afterDate = new DateTime(year, 12, 31);

                query = query.Where(m => m.Media.ReleaseDate > afterDate);
            }
            else if ((match = olderThanRegex.Match(trimmedToken)).Success)
            {
                var years = int.Parse(match.Groups[1].Value);
                var beforeDate = DateTime.Now.AddYears(-years);

                query = query.Where(m => m.Media.ReleaseDate < beforeDate);
            }
            else if (!string.IsNullOrWhiteSpace(trimmedToken))
            {
                query = query.Where(m => EF.Functions.ILike(m.Media.Title, $"%{token}%") || EF.Functions.ILike(m.Media.Description, $"%{token}%"));
            }

            var resultItems = query.OrderByDescending(m => m.AverageRating.HasValue)
                                   .ThenByDescending(m => m.AverageRating)
                                   .Skip(pageNumber * itemsPerPage)
                                   .Take(itemsPerPage);

            return (resultItems.Select(m => m.Media), CalculateNumberOfPages(query.Count(), itemsPerPage));
        }

        private int CalculateNumberOfPages(int totalNumber, int pageSize)
        {
            var fullPages = totalNumber / pageSize;
            var partialPages = (totalNumber % pageSize == 0) ? 0 : 1;

            return fullPages + partialPages;
        }
    }
}
