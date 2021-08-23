using Domain;

namespace DataLayer.Interfaces
{
    public interface IRatingsRepository : IGenericRepository<Rating>
    {
        public Rating GetUserRatingForMovie(int movieId, int userId);
        public Rating GetUserRatingForTVShow(int seriesId, int userId);
    }
}
