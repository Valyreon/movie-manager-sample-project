using System.Collections.Generic;
using Domain;

namespace ServiceLayer.Interfaces
{
    public interface IMovieService
    {
        IEnumerable<Movie> GetTopRatedMovies(int count);
    }
}
