using System;
using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace ServiceLayer
{
    public class TVShowsService : ITVShowsService
    {
        private readonly IUnitOfWork uow;
        private readonly IMappingService mappingService;

        public TVShowsService(IUnitOfWork uow, IMappingService mappingService)
        {
            this.uow = uow;
            this.mappingService = mappingService;
        }

        public TVShowDetailsResponse GetTVShowDetails(int id)
        {
            var series = uow.TVShows.GetMediaWithLoadedData(id);

            if (series == null)
            {
                throw new Exception("No item with that id."); // custom exception
            }

            return mappingService.MapTVShowToDetailsResponse(series);
        }

        public void Rate(RateRequest request, string userEmail)
        {
            var user = uow.Users.GetUserByEmail(userEmail);

            if (user == null)
            {
                throw new Exception("Invalid user."); // custom exception
            }

            var tvShow = uow.TVShows.GetById(request.MediaId);

            if (tvShow == null)
            {
                throw new Exception("Invalid mvoie id."); //Custom exception
            }

            var rating = uow.Ratings.GetUserRatingForTVShow(request.MediaId, user.Id);

            if (rating != null)
            {
                rating.Value = request.Value;
                uow.Commit();
                return;
            }

            rating = new Domain.Rating { UserId = user.Id, TVShowId = request.MediaId, Value = request.Value };
            uow.Ratings.Add(rating);
            uow.Commit();
        }

        public TVShowsPageResponse SearchTopRatedTVShows(string token, int pageNumber = 0, int itemsPerPage = 10)
        {
            var (PageItems, TotalNumberOfPages) = uow.TVShows.SearchTopRated(token, pageNumber, itemsPerPage);

            return new TVShowsPageResponse
            {
                Items = PageItems.Select(mappingService.MapTVShowToListItem),
                PageNumber = pageNumber + 1,
                PageSize = 10,
                TotalPages = TotalNumberOfPages
            };
        }
    }
}
