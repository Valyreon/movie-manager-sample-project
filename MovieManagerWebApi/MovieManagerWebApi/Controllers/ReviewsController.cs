using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagerWebApi.Extensions;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses.JsonModels;
using System.Collections.Generic;

namespace MovieManagerWebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsService reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            this.reviewsService = reviewsService;
        }

        [HttpPut]
        public ActionResult Rate([FromBody] ReviewRequest request)
        {
            var currentUserEmail = ControllerContext.HttpContext.User.GetUserEmail();
            reviewsService.Rate(request, currentUserEmail);
            return Ok();
        }

        [Route("{movieId}")]
        [HttpGet]
        public ActionResult<IEnumerable<ReviewData>> GetMovieReviews(int movieId, bool onlyCurrentUsersReview = false)
        {
            var currentUserEmail = ControllerContext.HttpContext.User.GetUserEmail();

            var result = onlyCurrentUsersReview
                                ? new ReviewData[] { reviewsService.GetCurrentUsersReviewForMovie(movieId, currentUserEmail) }
                                : reviewsService.GetAllReviewsForMovie(movieId, currentUserEmail);

            return Ok(result);
        }
    }
}
