using System;

namespace DataLayer.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMoviesRepository Movies { get; }
        int Commit();
    }
}
