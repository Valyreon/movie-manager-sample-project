using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Exceptions;

namespace MovieManagerWebApi.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = error switch
                {
                    InvalidLoginCredentialsException _ => (int)HttpStatusCode.Unauthorized,
                    EntityNotFoundException _ => (int)HttpStatusCode.NotFound,
                    UserAlreadyExistsException _ => (int)HttpStatusCode.Conflict,
                    PasswordTooWeakException _ => (int)HttpStatusCode.Forbidden,
                    _ => (int)HttpStatusCode.InternalServerError,// unhandled error
                };

                var message = response.StatusCode switch
                {
                    (int)HttpStatusCode.InternalServerError => "Something went wrong.",
                    _ => error?.Message
                };

                var result = JsonSerializer.Serialize(new { message = message });
                await response.WriteAsync(result);
            }
        }
    }
}
