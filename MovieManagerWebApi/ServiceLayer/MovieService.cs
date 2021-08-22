using System.Collections.Generic;
using DataLayer;
using Domain;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class MovieService : IMovieService
    {
        private readonly UnitOfWork uow;

        public MovieService(UnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<Movie> GetTopRatedMovies(int count)
        {
            return uow.Movies.GetTopRated(count);
        }
    }
}
