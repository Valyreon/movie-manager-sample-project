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
        public ActionResult<MediaPageResponse> SearchTopRatedMedia([FromQuery] SearchMediaRequest request)
        {
            return request.Movies
                ? mediaService.SearchTopRatedMovies(request.Token, request.PageNumber, request.PageSize)
                : mediaService.SearchTopRatedTVShows(request.Token, request.PageNumber, request.PageSize);
        }
    }
}
