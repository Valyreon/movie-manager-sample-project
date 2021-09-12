using ServiceLayer.Requests;
using ServiceLayer.Responses.JsonModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IRatingsService
    {
        void Rate(RateRequest request, string userEmail);
        IEnumerable<RatingData> GetAllRatingsForMovie(int movieId);
    }
}
