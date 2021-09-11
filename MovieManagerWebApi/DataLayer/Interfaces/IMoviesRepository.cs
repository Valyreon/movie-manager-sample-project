using DataLayer.Enums;
using DataLayer.Parameters;
using Domain;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public interface IMoviesRepository : IGenericRepository<Movie>
    {
         Movie GetMovieWithLoadedData(int id);
         (IEnumerable<Movie> PageItems, int TotalNumberOfPages) Page(MoviesPagingParameters parameters);
    }
}
