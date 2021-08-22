using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class MoviesRepository : MediaRepository<Movie>, IMoviesRepository
    {
        public MoviesRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
