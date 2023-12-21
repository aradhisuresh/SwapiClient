using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using Moq;
using SwapiClient.Application;
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
            iSwapiClientHelperMock.Setup(s => s.GetPeople(1)).Throws(new Exception("MockedException"));

            var wrapperLoggerMock = new Mock<ILogger<SwapiWrapper>>();

            SwapiWrapper wrapper = new SwapiWrapper(iSwapiClientHelperMock.Object, wrapperLoggerMock.Object);



            // Assert
            Assert.Throws<Exception>(() => wrapper.GetPersonsBasicInfo(1));
        }

        [Test]
        public void GetCharacters_WithData()
        {
            // Arrange
            var iSwapiClientHelperMock = new Mock<ISwapiHttpClientHelper>();
            var resp = new ValueTask<SwapiPeopleResponse>(new SwapiPeopleResponse { Results = new List<SwapiPeople> { new SwapiPeople(), new SwapiPeople() } });
            iSwapiClientHelperMock.Setup(s => s.GetPeople(1)).Returns(() => resp);

            var wrapperLoggerMock = new Mock<ILogger<SwapiWrapper>>();

            SwapiWrapper wrapper = new SwapiWrapper(iSwapiClientHelperMock.Object, wrapperLoggerMock.Object);


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

            var resp = new ValueTask<SwapiPeopleResponse>(new SwapiPeopleResponse { Results = new List<SwapiPeople> { new SwapiPeople(), new SwapiPeople() } });
            iSwapiClientHelperMock.Setup(s => s.GetPeople(1)).Returns(() => resp);

            var wrapperLoggerMock = new Mock<ILogger<SwapiWrapper>>();

            SwapiWrapper wrapper = new SwapiWrapper(iSwapiClientHelperMock.Object, wrapperLoggerMock.Object);

            var result = wrapper.GetPersonsDetailedInfo(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count() == 2);

            Assert.Pass();


        }
    }
}