using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface ITVShowsService
    {
        TVShowDetailsResponse GetTVShowDetails(int id);
        TVShowsPageResponse SearchTopRatedTVShows(string token, int pageNumber = 0, int itemsPerPage = 10);
        void Rate(RateRequest request, string userEmail);
    }
}
