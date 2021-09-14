using System;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Exceptions;

namespace MovieManagerWebApi.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception error)
            {
                LogToLocalFile(error);
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

                var result = JsonSerializer.Serialize(new { Error = new { message } });
                await response.WriteAsync(result);
            }
        }

        public static void LogToLocalFile(Exception ex)
        {
            const string logPath = @"C:\Users\Luka\Desktop\log.txt";
            if (File.Exists(logPath))
            {
                File.AppendAllText(logPath, "\n=================================================\n" + $"Message: {ex.Message}\n\n" + ex.StackTrace);
            }
        }
    }
}
