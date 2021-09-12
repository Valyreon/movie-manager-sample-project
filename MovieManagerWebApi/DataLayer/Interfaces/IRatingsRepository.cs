using Domain;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public interface IReviewsRepository : IGenericRepository<Review>
    {
        public Review GetUserReviewForMovie(int movieId, int userId);
        public IEnumerable<Review> GetAllReviewsForMovie(int movieId);
    }
}
