using Domain;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer
{
    public class MappingService : IMappingService
    {
        public MediaListItem MapMovieToListItem(Movie movie)
        {
            return new MediaListItem
            {
                Title = movie.Title,
                AverageRating = movie.AverageRating,
                ReleaseDate = movie.ReleaseDate,
                CoverPath = movie.CoverPath
            };
        }

        public MediaListItem MapTVShowToListItem(TVShow series)
        {
            return new TVShowListItem
            {
                Title = series.Title,
                AverageRating = series.AverageRating,
                ReleaseDate = series.ReleaseDate,
                CoverPath = series.CoverPath,
                EndDate = series.EndDate
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
