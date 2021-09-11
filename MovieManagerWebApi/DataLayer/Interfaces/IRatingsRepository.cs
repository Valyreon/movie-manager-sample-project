using Domain;

namespace DataLayer.Interfaces
{
    public interface IRatingsRepository : IGenericRepository<Rating>
    {
        public Rating GetRatingForMovie(int movieId, int userId);
    }
}
