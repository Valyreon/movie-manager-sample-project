using System;
using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
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
