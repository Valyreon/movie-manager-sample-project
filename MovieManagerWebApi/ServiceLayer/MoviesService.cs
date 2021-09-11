using System;
using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

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
                throw new Exception("No item with that id."); // custom exception
            }

            return mappingService.MapMovieToDetailsResponse(movie);
        }

        public void Rate(RateRequest request, string userEmail)
        {
            var user = uow.Users.GetUserByEmail(userEmail);

            if (user == null)
            {
                throw new Exception("Invalid user."); // custom exception
            }

            var movie = uow.Movies.GetById(request.MediaId);

            if (movie == null)
            {
                throw new Exception("Invalid mvoie id."); //Custom exception
            }

            var rating = uow.Ratings.GetRatingForMovie(request.MediaId, user.Id);

            if (rating != null)
            {
                rating.Value = request.Value;
                uow.Commit();
                return;
            }

            rating = new Domain.Rating { UserId = user.Id, MovieId = request.MediaId, Value = request.Value };
            uow.Ratings.Add(rating);
            uow.Commit();
        }

        public MoviesPageResponse SearchTopRatedMovies(MoviePageRequest request)
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
    }
}
