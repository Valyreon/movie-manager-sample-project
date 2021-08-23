using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService mediaService;

        public MoviesController(IMovieService mediaService)
        {
            this.mediaService = mediaService;
        }

        [HttpGet]
        public ActionResult<MovieDetailsResponse> GetMovieDetails([FromQuery][Required] int id)
        {
            return mediaService.GetMovieDetails(id);
        }

        [HttpGet]
        public ActionResult<MoviesPageResponse> SearchTopRatedMovies([FromQuery] SearchMediaRequest request)
        {
            return mediaService.SearchTopRatedMovies(request.Token, request.PageNumber, request.PageSize);
        }
    }
}
