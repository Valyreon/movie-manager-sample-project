using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService mediaService;

        public MediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [HttpGet]
        public ActionResult<MoviePageResponse> SearchTopRatedMovies([FromQuery] SearchMediaRequest request)
        {
            return mediaService.SearchTopRatedMovies(request.Token, request.PageNumber, request.PageSize);
        }

        [HttpGet]
        public ActionResult<TVShowPageResponse> SearchTopRatedTVShows([FromQuery] SearchMediaRequest request)
        {
            return mediaService.SearchTopRatedTVShows(request.Token, request.PageNumber, request.PageSize);
        }

        [HttpGet]
        public ActionResult<MediaDetailsResponse> GetMovieDetails([FromQuery][Required] int id)
        {
            return mediaService.GetMovieDetails(id);
        }

        [HttpGet]
        public ActionResult<MediaDetailsResponse> GetTVShowDetails([FromQuery][Required] int id)
        {
            return mediaService.GetTVShowDetails(id);
        }
    }
}
