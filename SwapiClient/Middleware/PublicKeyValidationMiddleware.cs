using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace SwapiClient.Middleware
{
    public class PublicKeyValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public PublicKeyValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //Check if url contains public. Only then apply this middleware.
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("public/"))
            {
                string userApiKey = context.Request.Headers[SwapiClient.Domain.Constant.GenericConstants.PublicApiKeyName];

                if (string.IsNullOrWhiteSpace(userApiKey))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }

                Guid guid;
                if (!Guid.TryParse(userApiKey, out guid))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }
            }

            await _next(context);
        }
    }
}
