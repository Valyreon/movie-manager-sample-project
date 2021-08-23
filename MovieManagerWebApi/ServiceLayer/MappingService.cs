using System;
using System.Linq;
using Domain;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer
{
    public class MappingService : IMappingService
    {
        public MediaListItem MapMovieToListItem(Movie movie)
        {
            return new MediaListItem
            {
                Id = movie.Id,
                Title = movie.Title,
                AverageRating = movie.AverageRating.HasValue ? Math.Round(movie.AverageRating.Value, 1) : (double?)null,
                ReleaseDate = movie.ReleaseDate,
                CoverPath = movie.CoverPath
            };
        }

        public MediaListItem MapTVShowToListItem(TVShow series)
        {
            return new TVShowListItem
            {
                Id = series.Id,
                Title = series.Title,
                AverageRating = series.AverageRating.HasValue ? Math.Round(series.AverageRating.Value, 1) : (double?)null,
                ReleaseDate = series.ReleaseDate,
                CoverPath = series.CoverPath,
                EndDate = series.EndDate,
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

        public MediaDetailsResponse MapMovieToDetailsResponse(Movie movie)
        {
            return new MediaDetailsResponse
            {
                Id = movie.Id,
                Actors = movie.Actors.Select(a => a.Name),
                Ratings = movie.Ratings.Select(MapRatingToRatingData),
                Description = movie.Description,
                AverageRating = movie.AverageRating.HasValue ? Math.Round(movie.AverageRating.Value, 1) : (double?)null,
                ReleaseDate = movie.ReleaseDate,
                CoverPath = movie.CoverPath,
                Title = movie.Title
            };
        }

        public MediaDetailsResponse MapTVShowToDetailsResponse(TVShow series)
        {
            return new MediaDetailsResponse
            {
                Id = series.Id,
                Actors = series.Actors.Select(a => a.Name),
                Ratings = series.Ratings.Select(MapRatingToRatingData),
                Description = series.Description,
                AverageRating = series.AverageRating.HasValue ? Math.Round(series.AverageRating.Value, 1) : (double?)null,
                ReleaseDate = series.ReleaseDate,
                CoverPath = series.CoverPath,
                Title = series.Title
            };
        }
    }
}
