using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;
using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace MovieManagerWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TVShowsController : ControllerBase
    {
        private readonly ITVShowsService tvShowsService;

        public TVShowsController(ITVShowsService mediaService)
        {
            tvShowsService = mediaService;
        }

        [HttpGet]
        public ActionResult<TVShowsPageResponse> SearchTopRatedTVShows([FromQuery] SearchMediaRequest request)
        {
            return tvShowsService.SearchTopRatedTVShows(request.Token, request.PageNumber, request.PageSize);
        }



        [HttpGet]
        public ActionResult<TVShowDetailsResponse> GetTVShowDetails([FromQuery][Required] int id)
        {
            return tvShowsService.GetTVShowDetails(id);
        }
    }
}
