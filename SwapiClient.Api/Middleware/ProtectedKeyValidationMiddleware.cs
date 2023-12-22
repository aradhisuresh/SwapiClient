using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SwapiClient.Middleware
{
    public class ProtectedKeyValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public ProtectedKeyValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.HasValue && context.Request.Path.Value.Contains("protected/"))
            {
                string protectedApiKey = context.Request.Headers[SwapiClient.Domain.Constant.GenericConstants.ProtectedApiKeyName];
                if (string.IsNullOrWhiteSpace(protectedApiKey))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }

                //Auth Header value splits the Content into scheme and Value separated by space.
                AuthenticationHeaderValue authInfo;
                if (AuthenticationHeaderValue.TryParse(protectedApiKey, out authInfo))
                {
                    if (authInfo == null || authInfo.Scheme?.ToLower() != "bearer")
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }

                    //Verify if valid Guid.
                    Guid guid;
                    if (!Guid.TryParse(authInfo.Parameter, out guid))
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
