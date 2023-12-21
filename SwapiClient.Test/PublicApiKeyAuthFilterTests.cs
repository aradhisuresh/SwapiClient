using Castle.Core.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Swapi.Core.Abstractions;
using Swapi.Core.Domain;
using SwapiClient.Controllers;
using System.Security.Claims;
using Microsoft.AspNetCore.Routing;

namespace SwapiClient.Test
{
    public class PublicApiKeyAuthFilterTests
    {
        public Mock<IFilterMetadata> filterMetadataMock = new Mock<IFilterMetadata>();

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void NoHeader_BadRequest()
        {
            // Arrange
            var filter = new PublicApiKeyAuthFilter();

            var httpContext = new DefaultHttpContext();
            // Add headers to the request
            httpContext.Request.Headers.Add("Authorization", "Bearer YourToken");

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
                new[] { filterMetadataMock.Object }
            );

            // Act
            filter.OnAuthorization(context);

            // Assert
            Assert.IsInstanceOf(typeof(BadRequestResult), context.Result);
        }

        [Test]
        public void EmptyHeader_UnauthorizedRequest()
        {
            // Arrange
            var filter = new PublicApiKeyAuthFilter();

            var httpContext = new DefaultHttpContext();
            // Add headers to the request
            httpContext.Request.Headers.Add("x-api-key", "");

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
                new[] { filterMetadataMock.Object }
            );

            // Act
            filter.OnAuthorization(context);

            // Assert
            Assert.IsInstanceOf(typeof(BadRequestResult), context.Result);
        }

        [Test]
        public void NoGuid_UnauthorizedRequest()
        {
            // Arrange
            var filter = new PublicApiKeyAuthFilter();

            var httpContext = new DefaultHttpContext();
            // Add headers to the request
            httpContext.Request.Headers.Add("x-api-key", "abcd");

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
                new[] { filterMetadataMock.Object }
            );

            // Act
            filter.OnAuthorization(context);

            // Assert
            Assert.IsInstanceOf(typeof(UnauthorizedResult), context.Result);
        }

        [Test]
        public void ValidGuid_Request()
        {
            // Arrange
            var filter = new PublicApiKeyAuthFilter();

            var httpContext = new DefaultHttpContext();
            // Add headers to the request
            httpContext.Request.Headers.Add("x-api-key", "c3a03c56-3404-494a-a0f6-8d43970aae30");

            var context = new AuthorizationFilterContext(
                new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
                new[] { filterMetadataMock.Object }
            );

            // Act
            filter.OnAuthorization(context);

            // Assert
            Assert.IsNull(context.Result);
        }
    }
}