using System;
using System.Linq;
using DataLayer.Enums;
using DataLayer.Parameters;
using Domain;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer
{
    public class MappingService : IMappingService
    {
        public MovieListItem MapMovieToListItem(Movie movie)
        {
            var averageRating = movie.CalculateAverageRating();

            var res = new MovieListItem
            {
                Id = movie.Id,
                Title = movie.Title,
                ReleaseYear = movie.ReleaseDate.Year,
                CoverPath = movie.CoverPath,
                AverageRating = averageRating.HasValue 
                                        ? Math.Round(averageRating.Value, 1) 
                                        : (double?)null
            };

            return res;
        }

        public RatingData MapRatingToRatingData(Rating rating)
        {
            return new RatingData
            {
                RatedBy = rating.User.Username,
                Value = rating.Value,
                RatedWhen = rating.ModifiedWhen.ToString()
            };
        }

        public MovieDetailsResponse MapMovieToDetailsResponse(Movie movie)
        {
            var averageRating = movie.CalculateAverageRating();

            var res = new MovieDetailsResponse
            {
                Id = movie.Id,
                Actors = movie.Actors.Select(a => a.Name),
                Description = movie.Description,
                AverageRating = averageRating.HasValue ? Math.Round(averageRating.Value, 1) : (double?)null,
                ReleaseYear = movie.ReleaseDate.Year,
                CoverPath = movie.CoverPath,
                Title = movie.Title
            };

            return res;
        }

        public MoviesPagingParameters MapPageRequestToParameters(MoviesPageRequest request)
        {
            return new MoviesPagingParameters
            {
                Token = request.Token,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                OrderBy = MapMoviesOrderByStringToEnum(request.OrderBy),
                Ascending = request.Ascending
            };
        }

        public MoviesOrderBy MapMoviesOrderByStringToEnum(string str)
        {
            return str.ToLower() switch
            {
                "title" => MoviesOrderBy.Title,
                "rating" => MoviesOrderBy.Rating,
                "release" => MoviesOrderBy.Release,
                _ => throw new NotSupportedException($"'{str}' is not an available MoviesOrderBy enum."),
            };
        }
    }
}
