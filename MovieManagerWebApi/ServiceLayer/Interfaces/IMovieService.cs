using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface IMovieService
    {
        MediaPageResponse SearchTopRatedMovies(string token, int pageNumber = 0, int itemsPerPage = 10);
    }
}
