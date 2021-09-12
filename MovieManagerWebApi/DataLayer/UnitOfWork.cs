using DataLayer.Interfaces;
using DataLayer.Repositories;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieDbContext context;

        private IMoviesRepository moviesRepository;
        private IReviewsRepository reviewsRepository;
        private IUsersRepository usersRepository;

        public UnitOfWork(MovieDbContext context)
        {
            this.context = context;
        }

        public IMoviesRepository Movies => moviesRepository ??= new MoviesRepository(context);
        public IReviewsRepository Reviews => reviewsRepository ??= new ReviewsRepository(context);
        public IUsersRepository Users => usersRepository ??= new UsersRepository(context);

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
