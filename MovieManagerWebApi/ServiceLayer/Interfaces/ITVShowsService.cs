using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface ITVShowsService
    {
        MediaPageResponse SearchTopRatedTVShows(string token, int pageNumber = 0, int itemsPerPage = 10);
    }
}
