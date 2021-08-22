using ServiceLayer.Requests;
using ServiceLayer.Responses;

namespace ServiceLayer.Interfaces
{
    public interface IAuthenticationService
    {
        public LoginResponse Login(LoginRequest request);
        public SignupResponse Signup(SignupRequest request);
    }
}
