using ServiceLayer.Requests;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IMoviesService
    {
        MovieDetailsResponse GetMovieDetails(int id);
        MoviesPageResponse Page(MoviesPageRequest request);
        IEnumerable<MovieListItem> GetAllMovies();
    }
}
