using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer
{
    public class MovieService : IMoviesService
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
            var movie = uow.Movies.GetMovieWithLoadedData(id);

            if (movie == null)
            {
                throw new EntityNotFoundException("Movie not found.");
            }

            return mappingService.MapMovieToDetailsResponse(movie);
        }

        public MoviesPageResponse Page(MoviesPageRequest request)
        {
            var pageParameters = mappingService.MapPageRequestToParameters(request);
            var (PageItems, TotalNumberOfPages) = uow.Movies.Page(pageParameters);

            return new MoviesPageResponse
            {
                Items = PageItems.Select(mappingService.MapMovieToListItem),
                PageNumber = request.PageNumber + 1,
                PageSize = request.PageSize,
                TotalPages = TotalNumberOfPages
            };
        }

        public IEnumerable<MovieListItem> GetAllMovies()
        {
            return uow.Movies.GetAllMoviesWithLoadedData().Select(mappingService.MapMovieToListItem);
        }
    }
}
