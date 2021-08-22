using System.Collections.Generic;
using Domain;

namespace DataLayer.Interfaces
{
    public interface IMoviesRepository : IGenericRepository<Movie>
    {
        Movie GetMovieWithLoadedData(int id);
        (IEnumerable<Movie> PageItems, int TotalNumberOfPages) SearchTopRated(string token = null, int page = 0, int itemsPerPage = 10);
    }
}
