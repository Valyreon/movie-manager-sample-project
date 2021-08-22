using System.Collections.Generic;
using Domain;

namespace DataLayer.Interfaces
{
    public interface IMediaRepository<T> : IGenericRepository<T> where T : Multimedia
    {
        T GetMediaWithLoadedData(int id);
        (IEnumerable<T> PageItems, int TotalNumberOfPages) SearchTopRated(string token = null, int page = 0, int itemsPerPage = 10);
    }
}
