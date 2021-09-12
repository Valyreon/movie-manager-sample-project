using System;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMoviesRepository Movies { get; }
        IUsersRepository Users { get; }
        IReviewsRepository Reviews { get; }
        int Commit();
    }
}
