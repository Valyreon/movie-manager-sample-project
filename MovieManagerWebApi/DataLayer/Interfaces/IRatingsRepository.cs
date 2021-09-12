using Domain;
using System.Collections.Generic;

namespace DataLayer.Interfaces
{
    public interface IRatingsRepository : IGenericRepository<Rating>
    {
        public Rating GetUserRatingForMovie(int movieId, int userId);
        public IEnumerable<Rating> GetAllRatingsForMovie(int movieId);
    }
}
