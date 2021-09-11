using System;
using System.Linq;
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
                RatedWhen = rating.ModifiedWhen.ToShortDateString()
            };
        }

        public MovieDetailsResponse MapMovieToDetailsResponse(Movie movie)
        {

            var averageRating = movie.CalculateAverageRating();

            var res = new MovieDetailsResponse
            {
                Id = movie.Id,
                Actors = movie.Actors.Select(a => a.Name),
                Ratings = movie.Ratings.Select(MapRatingToRatingData),
                Description = movie.Description,
                AverageRating = averageRating.HasValue ? Math.Round(averageRating.Value, 1) : (double?)null,
                ReleaseYear = movie.ReleaseDate.Year,
                CoverPath = movie.CoverPath,
                Title = movie.Title
            };

            return res;
        }

        public MoviesPagingParameters MapPageRequestToParameters(MoviePageRequest request)
        {
            return new MoviesPagingParameters
            {
                Token = request.Token,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                OrderBy = request.OrderBy,
                OrderDirection = request.OrderDirection
            };
        }
    }
}
