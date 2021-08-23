using System;
using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;

namespace ServiceLayer
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork uow;
        private readonly IMappingService mappingService;

        public MovieService(IUnitOfWork uow, IMappingService mappingService)
        {
            this.uow = uow;
            this.mappingService = mappingService;
        }

        public MovieDetailsResponse GetMovieDetails(int id)
        {
            var movie = uow.Movies.GetMediaWithLoadedData(id);

            if (movie == null)
            {
                throw new Exception("No item with that id."); // custom exception
            }

            return mappingService.MapMovieToDetailsResponse(movie);
        }

        public MoviesPageResponse SearchTopRatedMovies(string token, int pageNumber = 0, int itemsPerPage = 10)
        {
            var (PageItems, TotalNumberOfPages) = uow.Movies.SearchTopRated(token, pageNumber, itemsPerPage);

            return new MoviesPageResponse
            {
                Items = PageItems.Select(mappingService.MapMovieToListItem),
                PageNumber = pageNumber + 1,
                PageSize = 10,
                TotalPages = TotalNumberOfPages
            };
        }
    }
}
