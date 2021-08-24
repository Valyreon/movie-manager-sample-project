using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DataLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    internal class RatedMedia<T>
    {
        public T Media { get; internal set; }
        public double? AverageRating { get; internal set; }
    }

    public abstract class MediaRepository<T> : GenericRepository<T>, IMediaRepository<T> where T : Multimedia
    {
        private static readonly IReadOnlyDictionary<Regex, Func<IQueryable<RatedMedia<T>>, string, IQueryable<RatedMedia<T>>>> regexQueryDict
            = new Dictionary<Regex, Func<IQueryable<RatedMedia<T>>, string, IQueryable<RatedMedia<T>>>>
        {
                { new Regex("^([1-5]) stars$"), ApplySpecificStarQuery },
                { new Regex("^at least ([1-5]) star(s)?$"), ApplyAtLeastStarsQuery },
                { new Regex(@"^after (\d{4})$"), ApplyAfterYearQuery },
                { new Regex(@"^older than (\d+) years$"), ApplyOlderThanQuery }
        };

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
            var ratingIdSelector = typeof(Movie).IsAssignableFrom(typeof(T))
                                            ? ((Rating r) => r.Movie as T)
                                            : (Func<Rating, T>)((Rating r) => r.TVShow as T);

            var query = context.Set<T>().Include(m => m.Ratings)
                    .Select(m => new RatedMedia<T> { Media = m, AverageRating = m.Ratings.Average(r => r.Value) });

            var trimmedToken = token == null ? "" : token.Trim();
            var matchingRegexFound = false;
            foreach (var regex in regexQueryDict.Keys)
            {
                var match = regex.Match(trimmedToken);
                if (matchingRegexFound = match.Success)
                {
                    query = regexQueryDict[regex](query, trimmedToken);
                    break;
                }
            }

            if (!matchingRegexFound && !string.IsNullOrWhiteSpace(trimmedToken))
            {
                query = query.Where(
                    m => EF.Functions.Like(m.Media.Title, $"%{token}%")
                                || EF.Functions.Like(m.Media.Description, $"%{token}%"));
            }

            var resultItems = query.OrderByDescending(m => m.AverageRating.HasValue)
                                   .ThenByDescending(m => m.AverageRating)
                                   .Skip(pageNumber * itemsPerPage)
                                   .Take(itemsPerPage);

            return (resultItems.Select(m => m.Media), CalculateNumberOfPages(query.Count(), itemsPerPage));
        }

        private static IQueryable<RatedMedia<T>> ApplySpecificStarQuery(IQueryable<RatedMedia<T>> query, string numberOfStars)
        {
            var stars = int.Parse(numberOfStars);
            return query.Where(a => a.AverageRating > stars - 0.5 && a.AverageRating <= stars + 0.5);
        }

        private static IQueryable<RatedMedia<T>> ApplyAtLeastStarsQuery(IQueryable<RatedMedia<T>> query, string numberOfStars)
        {
            var stars = int.Parse(numberOfStars);
            return query.Where(a => a.AverageRating >= stars);
        }

        private static IQueryable<RatedMedia<T>> ApplyAfterYearQuery(IQueryable<RatedMedia<T>> query, string yearString)
        {
            var year = int.Parse(yearString);
            var afterDate = new DateTime(year, 12, 31);

            return query.Where(m => m.Media.ReleaseDate > afterDate);
        }

        private static IQueryable<RatedMedia<T>> ApplyOlderThanQuery(IQueryable<RatedMedia<T>> query, string yearString)
        {
            var years = int.Parse(yearString);
            var beforeDate = DateTime.Now.AddYears(-years);

            return query.Where(m => m.Media.ReleaseDate < beforeDate);
        }

        private int CalculateNumberOfPages(int totalNumber, int pageSize)
        {
            var fullPages = totalNumber / pageSize;
            var partialPages = (totalNumber % pageSize == 0) ? 0 : 1;

            return fullPages + partialPages;
        }
    }
}
