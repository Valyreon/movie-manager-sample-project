using DataLayer.Enums;
using DataLayer.Interfaces;
using DataLayer.Parameters;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DataLayer.Repositories
{
    internal class RatedMovie
    {
        public Movie Movie { get; internal set; }
        public double? AverageReview { get; internal set; }
    }

    public class MoviesRepository : GenericRepository<Movie>, IMoviesRepository
    {
        private static readonly IReadOnlyDictionary<Regex, Func<IQueryable<RatedMovie>, string, IQueryable<RatedMovie>>> regexQueryDict
            = new Dictionary<Regex, Func<IQueryable<RatedMovie>, string, IQueryable<RatedMovie>>>
        {
                { new Regex("^([1-5]) stars$"), ApplySpecificStarQuery },
                { new Regex("^at least ([1-5]) star(s)?$"), ApplyAtLeastStarsQuery },
                { new Regex(@"^after (\d{4})$"), ApplyAfterYearQuery },
                { new Regex(@"^older than (\d+) years$"), ApplyOlderThanQuery }
        };

        public MoviesRepository(MovieDbContext context) : base(context)
        {
            
        }

        public Movie GetMovieWithLoadedData(int id)
        {
            return context.Movies.Include(m => m.Reviews)
                                    .ThenInclude(r => r.User)
                                 .Include(m => m.Actors)
                                 .SingleOrDefault(m => m.Id == id);
        }

        public (IEnumerable<Movie> PageItems, int TotalNumberOfPages) Page(MoviesPagingParameters parameters)
        {
            var query = context.Movies.Include(m => m.Reviews)
                    .Select(m => new RatedMovie { Movie = m, AverageReview = m.Reviews.Average(r => r.Rating) });

            var trimmedToken = parameters.Token == null ? "" : parameters.Token.Trim();
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
                    m => EF.Functions.Like(m.Movie.Title, $"%{trimmedToken}%")
                                || EF.Functions.Like(m.Movie.Description, $"%{trimmedToken}%"));
            }

            // ordering and paging
            var resultItems = SetOrder(query, parameters.OrderBy, parameters.Ascending)
                                   .Skip(parameters.PageNumber * parameters.PageSize)
                                   .Take(parameters.PageSize);

            return (resultItems.Select(m => m.Movie), CalculateNumberOfPages(query.Count(), parameters.PageSize));
        }

        private IEnumerable<RatedMovie> SetOrder(IQueryable<RatedMovie> query, MoviesOrderBy orderBy, bool ascending)
        {
            switch(orderBy)
            {
                case MoviesOrderBy.Title:
                    Func<RatedMovie, string> titleSelector = r => r.Movie.Title;
                    return (ascending ? query.OrderBy(titleSelector)
                                      : query.OrderByDescending(titleSelector));
                case MoviesOrderBy.Review:
                    Func<RatedMovie, bool> reviewSelectorHasValue = r => r.AverageReview.HasValue;
                    Func<RatedMovie, double?> reviewSelector = r => r.AverageReview;
                    return ascending ? query.OrderBy(reviewSelectorHasValue)
                                            .ThenBy(reviewSelector)
                                     : query.OrderByDescending(reviewSelectorHasValue)
                                            .ThenByDescending(reviewSelector);
                case MoviesOrderBy.Release:
                    Func<RatedMovie, DateTimeOffset> releaseSelector = r => r.Movie.ReleaseDate;
                    return (ascending ? query.OrderBy(releaseSelector)
                                      : query.OrderByDescending(releaseSelector));
                default:
                    throw new NotSupportedException("Unhandled enum.");
            }
        }

        private static IQueryable<RatedMovie> ApplySpecificStarQuery(IQueryable<RatedMovie> query, string numberOfStars)
        {
            var stars = int.Parse(numberOfStars);
            return query.Where(a => a.AverageReview > stars - 0.5 && a.AverageReview <= stars + 0.5);
        }

        private static IQueryable<RatedMovie> ApplyAtLeastStarsQuery(IQueryable<RatedMovie> query, string numberOfStars)
        {
            var stars = int.Parse(numberOfStars);
            return query.Where(a => a.AverageReview >= stars);
        }

        private static IQueryable<RatedMovie> ApplyAfterYearQuery(IQueryable<RatedMovie> query, string yearString)
        {
            var year = int.Parse(yearString);
            var afterDate = new DateTime(year, 12, 31);

            return query.Where(m => m.Movie.ReleaseDate > afterDate);
        }

        private static IQueryable<RatedMovie> ApplyOlderThanQuery(IQueryable<RatedMovie> query, string yearString)
        {
            var years = int.Parse(yearString);
            var beforeDate = DateTime.Now.AddYears(-years);

            return query.Where(m => m.Movie.ReleaseDate < beforeDate);
        }

        private int CalculateNumberOfPages(int totalNumber, int pageSize)
        {
            var fullPages = totalNumber / pageSize;
            var partialPages = (totalNumber % pageSize == 0) ? 0 : 1;

            return fullPages + partialPages;
        }
    }
}
