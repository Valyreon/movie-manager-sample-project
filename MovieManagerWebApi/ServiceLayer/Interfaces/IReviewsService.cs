using ServiceLayer.Requests;
using ServiceLayer.Responses.JsonModels;
using System.Collections.Generic;

namespace ServiceLayer.Interfaces
{
    public interface IReviewsService
    {
        void Rate(ReviewRequest request, string userEmail);
        IEnumerable<ReviewData> GetAllReviewsForMovie(int movieId);
    }
}
