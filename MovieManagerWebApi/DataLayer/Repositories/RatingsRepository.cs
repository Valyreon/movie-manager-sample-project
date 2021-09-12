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

        public IEnumerable<Review> GetAllReviewsForMovie(int movieId)
        {
            return context.Reviews.Where(r => r.MovieId == movieId);
        }

        public Review GetUserReviewForMovie(int movieId, int userId)
        {
            return context.Reviews.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefault();
        }
    }
}
