using System;
using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
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
                throw new Exception("No item with that id."); // TODO: custom exception
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
                PageSize = 10,
                TotalPages = TotalNumberOfPages
            };
        }

        public IEnumerable<MovieListItem> GetAllMovies()
        {
            return uow.Movies.GetAll().Select(mappingService.MapMovieToListItem);
        }
    }
}