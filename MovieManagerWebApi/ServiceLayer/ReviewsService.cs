using System.Collections.Generic;
using System.Linq;
using DataLayer.Interfaces;
using ServiceLayer.Exceptions;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses.JsonModels;

namespace ServiceLayer
{
    public class ReviewsService : IReviewsService
    {
        private readonly IUnitOfWork uow;
        private readonly IMappingService mappingService;

        public ReviewsService(IUnitOfWork uow, IMappingService mappingService)
        {
            this.uow = uow;
            this.mappingService = mappingService;
        }

        public void Rate(ReviewRequest request, string userEmail)
        {
            // get the user with specified email (logged in user)
            var user = uow.Users.GetUserByEmail(userEmail);

            // check if user exists
            if (user == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            // get the specified movie with id
            var movie = uow.Movies.GetById(request.MovieId);

            // check if movie exists
            if (movie == null)
            {
                throw new EntityNotFoundException("Movie not found.");
            }

            // get review by this user for speicfied movie
            var review = uow.Reviews.GetUserReviewForMovie(request.MovieId, user.Id);

            // if user has already rated this movie update the review to new value
            if (review != null)
            {
                review.Rating = request.Rating;
                uow.Commit();
                return;
            }

            // otherwise create new review
            review = new Domain.Review { UserId = user.Id, MovieId = request.MovieId, Rating = request.Rating };
            uow.Reviews.Add(review);
            uow.Commit();
        }

        public IEnumerable<ReviewData> GetAllReviewsForMovie(int movieId, string userEmail)
        {
            // get the user with specified email (logged in user)
            var user = uow.Users.GetUserByEmail(userEmail);

            // check if user exists
            if (user == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            return uow.Reviews.GetAllReviewsForMovie(movieId, user.Id).Select(mappingService.MapReviewToReviewData);
        }

        public ReviewData GetCurrentUsersReviewForMovie(int movieId, string userEmail)
        {
            // get the user with specified email (logged in user)
            var user = uow.Users.GetUserByEmail(userEmail);

            // check if user exists
            if (user == null)
            {
                throw new EntityNotFoundException("User not found.");
            }

            return mappingService.MapReviewToReviewData(
                uow.Reviews.GetUserReviewForMovie(movieId, user.Id)
            );
        }
    }
}
