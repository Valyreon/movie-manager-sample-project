using DataLayer.Parameters;
using Domain;
using ServiceLayer.Requests;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer.Interfaces
{
    public interface IMappingService
    {
        MovieListItem MapMovieToListItem(Movie movie);
        MovieDetailsResponse MapMovieToDetailsResponse(Movie movie);
        MoviesPagingParameters MapPageRequestToParameters(MoviesPageRequest request);
        ReviewData MapReviewToReviewData(Review review);
    }
}
