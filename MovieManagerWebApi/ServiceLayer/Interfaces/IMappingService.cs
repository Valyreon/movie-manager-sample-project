using Domain;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Interfaces
{
    public interface IMappingService
    {
        MovieListItem MapMovieToListItem(Movie movie);
        MovieDetailsResponse MapMovieToMovieDetailsResponse(Movie movie);
    }
}
