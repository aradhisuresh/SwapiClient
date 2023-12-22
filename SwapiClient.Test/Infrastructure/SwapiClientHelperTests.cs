using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using SwapiClient.Application;
using SwapiClient.Controllers;
using SwapiClient.Infrastructure;
using System.Net.Http;
using System.Net.Http.Json;

namespace SwapiClient.Test
{
    public class SwapiClientHelperTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetPeople()
        {
            // Arrange
            var httpClient = new Mock<IHttpClientFactory>();

            ISwapiHttpClientHelper clientHelper = new SwapiHttpClientHelper(httpClient.Object);



            // Assert
            var getPeopleResp = clientHelper.GetPeople(1);

            
        }
    }
}