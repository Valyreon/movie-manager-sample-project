using Domain;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Interfaces
{
    public interface IMappingService
    {
        MovieListItem MapMovieToListItem(Movie movie);
        TVShowListItem MapTVShowToListItem(TVShow series);
        MovieDetailsResponse MapMovieToDetailsResponse(Movie movie);
        TVShowDetailsResponse MapTVShowToDetailsResponse(TVShow series);
    }
}
