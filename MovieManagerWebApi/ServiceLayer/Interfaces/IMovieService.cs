using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface IMovieService
    {
        MoviePageResponse SearchTopRatedMovies(string token, int pageNumber = 0, int itemsPerPage = 10);
        MovieDetailsResponse GetMovieDetails(int id);
    }
}
