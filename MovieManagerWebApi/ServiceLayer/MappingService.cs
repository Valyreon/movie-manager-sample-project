using System.Linq;
using Domain;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer
{
    public class MappingService : IMappingService
    {
        public MovieListItem MapMovieToListItem(Movie movie)
        {
            return new MovieListItem
            {
                Title = movie.Title,
                AverageRating = movie.AverageRating,
                ReleaseDate = movie.ReleaseDate,
                CoverPath = movie.CoverPath
            };
        }

        public MovieDetailsResponse MapMovieToMovieDetailsResponse(Movie movie)
        {
            return new MovieDetailsResponse
            {
                Title = movie.Title,
                AverageRating = movie.AverageRating,
                ReleaseDate = movie.ReleaseDate,
                CoverPath = movie.CoverPath,
                Description = movie.Description,
                Actors = movie.Actors?.Select(a => a.Name),
                Ratings = movie.Ratings.Select(MapRatingToRatingData)
            };
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
    }
}
