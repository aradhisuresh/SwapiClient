using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;
using SwapiClient.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SwapiClient.Test.Api
{
    public class ProtectedKeyValidationMiddlewareTests
    {
        public ProtectedKeyValidationMiddlewareTests()
        {
        }

        private async Task<IHost> GetHost()
        {
            var host = await new HostBuilder()
       .ConfigureWebHost(webBuilder =>
       {
           webBuilder
                .UseTestServer()
                .ConfigureServices(services =>
                {

                })
                .Configure(app =>
                {
                    app.UseMiddleware<ProtectedKeyValidationMiddleware>();
                });
       })
       .StartAsync();


            return host;
        }

        private TestServer GetServer(IHost host)
        {

            var server = host.GetTestServer();
            server.BaseAddress = new Uri("https://example.com/A/Path/");

            return server;
        }

        [Test]
        public async Task NoHeader()
        {
            var host = await GetHost();
            var server = GetServer(host);


            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Get;
                c.Request.Path = "/swapi/protected/characters/download";
                c.Request.QueryString = new QueryString("?page=1");
            });


            var response = await host.GetTestClient().GetAsync("/");

            Assert.IsTrue(400 == context.Response.StatusCode);
        }


        [Test]
        public async Task EmptyHeader()
        {
            var host = await GetHost();
            var server = GetServer(host);

            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Get;
                c.Request.Path = "/swapi/protected/characters/download";
                c.Request.QueryString = new QueryString("?page=1");
                c.Request.Headers.Add(SwapiClient.Domain.Constant.GenericConstants.PublicApiKeyName, "");
            });


            var response = await host.GetTestClient().GetAsync("/");

            Assert.IsTrue(400 == context.Response.StatusCode);
        }
        [Test]
        public async Task InvalidGuidHeader()
        {

            var host = await GetHost();
            var server = GetServer(host);

            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Get;
                c.Request.Path = "/swapi/protected/characters/download";
                c.Request.QueryString = new QueryString("?page=1");
                c.Request.Headers.Add(SwapiClient.Domain.Constant.GenericConstants.PublicApiKeyName, "Bearer ");
            });


            var response = await host.GetTestClient().GetAsync("/");

            Assert.IsTrue(400 == context.Response.StatusCode);
        }
        [Test]
        public async Task ValidGuidHeader()
        {
            var host = await GetHost();
            var server = GetServer(host);

            var context = await server.SendAsync(c =>
            {
                c.Request.Method = HttpMethods.Get;
                c.Request.Path = "/swapi/public/characters";
                c.Request.QueryString = new QueryString("?page=1");
                c.Request.Headers.Add(SwapiClient.Domain.Constant.GenericConstants.PublicApiKeyName, "Bearer 47f1cb5d-eb9b-4fd3-96f7-65e651cd0093");
            });


            var response = await host.GetTestClient().GetAsync("/");

            Assert.IsTrue(400 != context.Response.StatusCode);
            Assert.IsTrue(401 != context.Response.StatusCode);
        }
    }
}