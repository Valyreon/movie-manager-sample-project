using System.Collections.Generic;
using Domain;

namespace DataLayer.Interfaces
{
    public interface IMoviesRepository : IGenericRepository<Movie>
    {
        IEnumerable<Movie> GetTopRated(int count = 10);
        IEnumerable<Movie> Search(string token);
    }
}
