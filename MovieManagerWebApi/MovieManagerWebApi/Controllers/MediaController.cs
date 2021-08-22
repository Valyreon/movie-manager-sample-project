using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Responses;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService mediaService;

        public MediaController(IMediaService mediaService)
        {
            this.mediaService = mediaService;
        }

        [HttpGet]
        public ActionResult<MediaPageResponse> SearchTopRatedMedia(
            [FromQuery] bool movies = true,
            [FromQuery] string token = null,
            [FromQuery] int pageNumber = 0,
            [FromQuery] int pageSize = 10)
        {
            return movies
                ? mediaService.SearchTopRatedMovies(token, pageNumber, pageSize)
                : mediaService.SearchTopRatedTVShows(token, pageNumber, pageSize);
        }
    }
}
