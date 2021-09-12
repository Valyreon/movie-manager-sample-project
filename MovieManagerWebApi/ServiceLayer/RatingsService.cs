using DataLayer.Interfaces;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses.JsonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public class RatingsService : IRatingsService
    {
        private readonly IUnitOfWork uow;
        private readonly IMappingService mappingService;

        public RatingsService(IUnitOfWork uow, IMappingService mappingService)
        {
            this.uow = uow;
            this.mappingService = mappingService;
        }

        public void Rate(RateRequest request, string userEmail)
        {
            // get the user with specified email (logged in user)
            var user = uow.Users.GetUserByEmail(userEmail);

            // check if user exists
            if (user == null)
            {
                throw new Exception("Invalid user."); // TODO: custom exception
            }

            // get the specified movie with id
            var movie = uow.Movies.GetById(request.MovieId);

            // check if movie exists
            if (movie == null)
            {
                throw new Exception("Invalid mvoie id."); //TODO: custom exception
            }

            // get rating by this user for speicfied movie
            var rating = uow.Ratings.GetUserRatingForMovie(request.MovieId, user.Id);

            // if user has already rated this movie update the rating to new value
            if (rating != null)
            {
                rating.Value = request.Value;
                uow.Commit();
                return;
            }

            // otherwise create new rating
            rating = new Domain.Rating { UserId = user.Id, MovieId = request.MovieId, Value = request.Value };
            uow.Ratings.Add(rating);
            uow.Commit();
        }

        public IEnumerable<RatingData> GetAllRatingsForMovie(int movieId)
        {
            return uow.Ratings.GetAllRatingsForMovie(movieId).Select(mappingService.MapRatingToRatingData);
        }
    }
}
