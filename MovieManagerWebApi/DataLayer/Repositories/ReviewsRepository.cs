using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class ReviewsRepository : GenericRepository<Review>, IReviewsRepository
    {
        public ReviewsRepository(MovieDbContext context) : base(context)
        {
        }
    }
}
