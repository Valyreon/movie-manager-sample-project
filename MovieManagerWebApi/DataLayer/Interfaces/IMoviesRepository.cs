using System.Collections.Generic;
using Domain;

namespace DataLayer.Interfaces
{
    public interface IMoviesRepository : IGenericRepository<Movie>
    {
        IEnumerable<Movie> GetTopRatedMovies(int count = 10);
        IEnumerable<Movie> GetPage(string token, string orderBy, bool ascending, int pageNumber = 0, int pageCount = 10);
    }
}
