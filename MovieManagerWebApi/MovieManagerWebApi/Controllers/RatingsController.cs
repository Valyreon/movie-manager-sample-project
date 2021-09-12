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
    public class RatingsController : ControllerBase
    {
        private readonly IRatingsService ratingsService;

        public RatingsController(IRatingsService ratingsService)
        {
            this.ratingsService = ratingsService;
        }

        [Route("rate-movie")]
        [HttpPut]
        public ActionResult Rate([FromBody] RateRequest request)
        {
            var currentUserEmail = ControllerContext.HttpContext.User.GetUserEmail();
            ratingsService.Rate(request, currentUserEmail);
            return Ok();
        }

        [Route("{movieId}")]
        [HttpGet]
        public ActionResult<IEnumerable<RatingData>> GetMovieRatings(int movieId)
        {
            var currentUserEmail = ControllerContext.HttpContext.User.GetUserEmail();
            return Ok(ratingsService.GetAllRatingsForMovie(movieId));
        }
    }
}
