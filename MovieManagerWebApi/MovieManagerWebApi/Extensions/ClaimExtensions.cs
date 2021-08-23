using System.Security.Claims;

namespace MovieManagerWebApi.Extensions
{
    public static class ClaimExtensions
    {
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            var emailClaim = user.FindFirst(ClaimTypes.Email);

            if (emailClaim != null)
            {
                return emailClaim.Value;
            }

            return null;
        }
    }
}
