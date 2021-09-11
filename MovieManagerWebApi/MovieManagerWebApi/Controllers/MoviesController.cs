using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieManagerWebApi.Extensions;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService movieService;

        public MoviesController(IMoviesService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public ActionResult<MovieDetailsResponse> GetMovieDetails([FromQuery][Required] int id)
        {
            return movieService.GetMovieDetails(id);
        }

        [HttpGet]
        public ActionResult<MoviesPageResponse> SearchTopRatedMovies([FromQuery] MoviePageRequest request)
        {
            return movieService.SearchTopRatedMovies(request);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Rate([FromBody] RateRequest request)
        {
            var currentUserEmail = ControllerContext.HttpContext.User.GetUserEmail();
            movieService.Rate(request, currentUserEmail);
            return Ok();
        }
    }
}
