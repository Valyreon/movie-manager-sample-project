using System.Collections.Generic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService movieService;

        public MoviesController(MovieService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        [Route("top-rated")]
        public IEnumerable<Movie> TopRatedMovies([FromQuery] int count)
        {
            return movieService.GetTopRatedMovies(count);
        }
    }
}
