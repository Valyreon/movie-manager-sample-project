using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface IMoviesService
    {
        MovieDetailsResponse GetMovieDetails(int id);
        MoviesPageResponse SearchTopRatedMovies(SearchMediaRequest request);
        void Rate(RateRequest request, string userEmail);
    }
}
