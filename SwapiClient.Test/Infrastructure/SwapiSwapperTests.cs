using Castle.Core.Logging;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using SwapiClient.Application;
using SwapiClient.Application.Query;
using SwapiClient.Controllers;
using SwapiClient.Domain;
using SwapiClient.Infrastructure;

namespace SwapiClient.Test
{
    public class SwapiSwapperTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void GetCharacters_WithException()
        {
            // Arrange
            var iSwapiClientHelperMock = new Mock<ISwapiHttpClientHelper>();
            var mediatorMock = new Mock<IMediator>();
            iSwapiClientHelperMock.Setup(s => s.GetPeople(1)).Throws(new Exception("MockedException"));

            var wrapperLoggerMock = new Mock<ILogger<SwapiWrapper>>();

            SwapiWrapper wrapper = new SwapiWrapper(mediatorMock.Object, wrapperLoggerMock.Object);



            // Assert
            Assert.Throws<Exception>(() => wrapper.GetPersonsBasicInfo(1));
        }

        [Test]
        public void GetCharacters_WithData()
        {
            // Arrange
            var iSwapiClientHelperMock = new Mock<ISwapiHttpClientHelper>();
            var mediatorMock = new Mock<IMediator>();

            var respObj = new SwapiPeopleResponse { Results = new List<SwapiPeople> { new SwapiPeople(), new SwapiPeople() } };
            var respTask = new Task<SwapiPeopleResponse>(() => respObj);
            iSwapiClientHelperMock.Setup(s => s.GetPeople(1)).Returns(() => respTask);
            mediatorMock.Setup(s => s.Send(It.IsAny<GetPeopleQuery>(), CancellationToken.None)).ReturnsAsync(() => respObj);

            var wrapperLoggerMock = new Mock<ILogger<SwapiWrapper>>();

            SwapiWrapper wrapper = new SwapiWrapper(mediatorMock.Object, wrapperLoggerMock.Object);


            var result = wrapper.GetPersonsBasicInfo(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);

            Assert.Pass();
        }

        [Test]
        public void GetCharacterDetails_WithData()
        {
            // Arrange
            var iSwapiClientHelperMock = new Mock<ISwapiHttpClientHelper>();
            var mediatorMock = new Mock<IMediator>();

            var respObj = new SwapiPeopleResponse { Results = new List<SwapiPeople> { new SwapiPeople(), new SwapiPeople() } };
            var respTask = new Task<SwapiPeopleResponse>(() => respObj);
            iSwapiClientHelperMock.Setup(s => s.GetPeople(1)).Returns(() => respTask);
            mediatorMock.Setup(s => s.Send(It.IsAny<GetPeopleQuery>(), CancellationToken.None)).ReturnsAsync(() => respObj);

            var wrapperLoggerMock = new Mock<ILogger<SwapiWrapper>>();

            SwapiWrapper wrapper = new SwapiWrapper(mediatorMock.Object, wrapperLoggerMock.Object);

            var result = wrapper.GetPersonsDetailedInfo(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);

            Assert.Pass();


        }
    }
}