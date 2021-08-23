using System.Linq;
using DataLayer.Interfaces;
using Domain;

namespace DataLayer.Repositories
{
    public class RatingsRepository : GenericRepository<Rating>, IRatingsRepository
    {
        public RatingsRepository(MovieDbContext context) : base(context)
        {
        }

        public Rating GetUserRatingForMovie(int movieId, int userId)
        {
            return context.Ratings.Where(r => r.UserId == userId && r.MovieId == movieId).SingleOrDefault();
        }

        public Rating GetUserRatingForTVShow(int seriesId, int userId)
        {
            return context.Ratings.Where(r => r.UserId == userId && r.TVShowId == seriesId).SingleOrDefault();
        }
    }
}
