using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface IMovieService
    {
        MovieDetailsResponse GetMovieDetails(int id);
        MoviesPageResponse SearchTopRatedMovies(string token, int pageNumber = 0, int itemsPerPage = 10);
    }
}
