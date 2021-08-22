using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;

        public MoviesController(IMovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public MovieDetailsResponse MovieDetails([FromQuery] int id)
        {
            return movieService.GetMovieDetails(id);
        }

        [HttpGet]
        public MoviePageResponse SearchTopRatedMovies(
            [FromQuery] string token,
            [FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 10)
        {
            return movieService.SearchTopRatedMovies(token, pageNumber, pageSize);
        }
    }
}
