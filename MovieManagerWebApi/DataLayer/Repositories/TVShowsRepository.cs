using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class TVShowsRepository : MediaRepository<TVShow>, ITVShowsRepository
    {
        public TVShowsRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
