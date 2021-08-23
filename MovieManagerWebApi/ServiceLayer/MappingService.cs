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
        public MovieListItem MapMovieToListItem(Movie movie)
        {
            return MapMediaToListItem(movie) as MovieListItem;
        }

        public TVShowListItem MapTVShowToListItem(TVShow series)
        {
            var res = MapMediaToListItem(series) as TVShowListItem;
            res.EndYear = series.EndDate.Year;
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
            return MapMediaToDetailsResponse(movie) as MovieDetailsResponse;
        }

        public TVShowDetailsResponse MapTVShowToDetailsResponse(TVShow series)
        {
            var res = MapMediaToDetailsResponse(series) as TVShowDetailsResponse;
            res.EndYear = series.EndDate.Year;
            res.NumberOfSeasons = series.NumberOfSeasons;
            return res;
        }

        private MediaDetailsResponse MapMediaToDetailsResponse(Multimedia media)
        {
            var res = media is Movie
                            ? new MovieDetailsResponse()
                            : (MediaDetailsResponse)new TVShowDetailsResponse();

            res.Id = media.Id;
            res.Actors = media.Actors.Select(a => a.Name);
            res.Ratings = media.Ratings.Select(MapRatingToRatingData);
            res.Description = media.Description;
            res.AverageRating = media.AverageRating.HasValue ? Math.Round(media.AverageRating.Value, 1) : (double?)null;
            res.ReleaseYear = media.ReleaseDate.Year;
            res.CoverPath = media.CoverPath;
            res.Title = media.Title;

            return res;
        }

        private MediaListItem MapMediaToListItem(Multimedia media)
        {
            var res = media is Movie
                            ? new MovieListItem()
                            : (MediaListItem)new TVShowListItem();

            res.Id = media.Id;
            res.Title = media.Title;
            res.ReleaseYear = media.ReleaseDate.Year;
            res.CoverPath = media.CoverPath;
            res.AverageRating = media.AverageRating;
            return res;
        }
    }
}
