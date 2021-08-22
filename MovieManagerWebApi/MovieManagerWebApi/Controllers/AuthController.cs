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
        public LoginResponse Login([FromBody] LoginRequest request)
        {
            return authenticationService.Login(request);
        }

        [HttpPost]
        public SignupResponse Signup([FromBody] SignupRequest request)
        {
            return authenticationService.Signup(request);
        }
    }
}
