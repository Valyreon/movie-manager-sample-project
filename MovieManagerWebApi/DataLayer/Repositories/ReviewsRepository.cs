using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class RatingsRepository : GenericRepository<Rating>, IRatingsRepository
    {
        public RatingsRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
