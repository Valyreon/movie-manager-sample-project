using DataLayer.Interfaces;
using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieDbContext context;

        private IMoviesRepository moviesRepository;
        private IRatingsRepository reviewsRepository;
        private IUsersRepository usersRepository;
        private ITVShowsRepository tvShowsRepository;

        public UnitOfWork(MovieDbContext context)
        {
            this.context = context;
        }

        public IMoviesRepository Movies => moviesRepository ??= new MoviesRepository(context);
        public IRatingsRepository Ratings => reviewsRepository ??= new RatingsRepository(context);
        public IUsersRepository Users => usersRepository ??= new UsersRepository(context);
        public ITVShowsRepository TVShows => tvShowsRepository ??= new TVShowsRepository(context);

        public int Commit()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
