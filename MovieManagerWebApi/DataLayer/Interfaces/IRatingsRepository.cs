using Domain;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public interface IReviewsRepository : IGenericRepository<Review>
    {
        Review GetUserReviewForMovie(int movieId, int userId);
        IEnumerable<Review> GetAllReviewsForMovie(int movieId, int? excludeUserId = null);
    }
}
