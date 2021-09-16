using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class ReviewsRepository : GenericRepository<Review>, IReviewsRepository
    {
        public ReviewsRepository(MovieDbContext context) : base(context)
        {
        }

        public IEnumerable<Review> GetAllReviewsForMovie(int movieId, int? excludeUserId = null)
        {
            var result = context.Reviews.Where(r => r.MovieId == movieId);

            if(excludeUserId.HasValue)
            {
                result = result.Where(r => r.UserId != excludeUserId.Value);
            }

            return result;
        }

        public Review GetUserReviewForMovie(int movieId, int userId)
        {
            return context.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefault();
        }
    }
}
