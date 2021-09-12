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
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost]
        public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
        {
            return authenticationService.Login(request);
        }

        [HttpPost]
        public ActionResult Signup([FromBody] SignupRequest request)
        {
            authenticationService.Signup(request);
            return Ok();
        }
    }
}
