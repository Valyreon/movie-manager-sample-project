using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;

namespace ServiceLayer
{
    public class MediaService : IMediaService
    {
        private readonly IUnitOfWork uow;
        private readonly IMappingService mappingService;

        public MediaService(IUnitOfWork uow, IMappingService mappingService)
        {
            this.uow = uow;
            this.mappingService = mappingService;
        }

        public MediaPageResponse SearchTopRatedMovies(string token, int pageNumber = 0, int itemsPerPage = 10)
        {
            var (PageItems, TotalNumberOfPages) = uow.Movies.SearchTopRated(token, pageNumber, itemsPerPage);

            return new MediaPageResponse
            {
                Items = PageItems.Select(mappingService.MapMovieToListItem),
                PageSize = itemsPerPage,
                PageNumber = pageNumber + 1,
                TotalNumberOfPages = TotalNumberOfPages
            };
        }

        public MediaPageResponse SearchTopRatedTVShows(string token, int pageNumber = 0, int itemsPerPage = 10)
        {
            var (PageItems, TotalNumberOfPages) = uow.TVShows.SearchTopRated(token, pageNumber, itemsPerPage);

            return new MediaPageResponse
            {
                Items = PageItems.Select(mappingService.MapTVShowToListItem),
                PageSize = itemsPerPage,
                PageNumber = pageNumber + 1,
                TotalNumberOfPages = TotalNumberOfPages
            };
        }
    }
}
