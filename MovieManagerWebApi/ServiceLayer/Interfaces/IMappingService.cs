using Domain;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Interfaces
{
    public interface IMappingService
    {
        MediaListItem MapMovieToListItem(Movie movie);
        MediaListItem MapTVShowToListItem(TVShow series);
    }
}
