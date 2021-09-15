using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;
using ServiceLayer.Responses.JsonModels;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService movieService;

        public MoviesController(IMoviesService movieService)
        {
            this.movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieListItem>> GetAllMovies()
        {
            return Ok(movieService.GetAllMovies());
        }

        [Route("{id}")]
        [HttpGet]
        public ActionResult<MovieDetailsResponse> GetMovieDetails([Required] int id)
        {
            return movieService.GetMovieDetails(id);
        }

        [Route("page")]
        [HttpGet]
        public ActionResult<MoviesPageResponse> GetMoviesPage([FromQuery] MoviesPageRequest request)
        {
            return movieService.Page(request);
        }
    }
}
