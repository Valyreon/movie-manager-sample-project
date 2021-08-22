using System;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMoviesRepository Movies { get; }
        IUsersRepository Users { get; }
        ITVShowsRepository TVShows { get; }
        IRatingsRepository Ratings { get; }
        int Commit();
    }
}
