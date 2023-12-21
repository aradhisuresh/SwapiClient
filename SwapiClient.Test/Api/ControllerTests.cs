using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using SwapiClient.Application;
using SwapiClient.Controllers;
using SwapiClient.Domain;

namespace SwapiClient.Test.Api
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetCharacters_WithException()
        {
            // Arrange
            var iSwapiWrapperMock = new Mock<ISwapiWrapper>();
            iSwapiWrapperMock.Setup(s => s.GetPersonsBasicInfo(1)).Throws(new Exception("MockedException"));

            var controllerLoggerMock = new Mock<ILogger<SwapiClientController>>();

            var controller = new SwapiClientController(controllerLoggerMock.Object, iSwapiWrapperMock.Object);


            // Assert
            Assert.Throws<Exception>(() => controller.GetCharacters(1));
        }

        [Test]
        public void GetCharacterDetails_WithData()
        {
            // Arrange
            var iSwapiWrapperMock = new Mock<ISwapiWrapper>();
            iSwapiWrapperMock.Setup(s => s.GetPersonsDetailedInfo(1)).Returns(() => new List<PersonDetailedInfo> { new PersonDetailedInfo(), new PersonDetailedInfo() });

            var controllerLoggerMock = new Mock<ILogger<SwapiClientController>>();

            var controller = new SwapiClientController(controllerLoggerMock.Object, iSwapiWrapperMock.Object);

            var result = controller.GetCharactersDownload(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);

            Assert.Pass();
        }
    }
}