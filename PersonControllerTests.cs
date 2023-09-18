using DotNetTest.Controllers;
using DotNetTest.Controllers.Responses;
using DotNetTest.Entities;
using DotNetTest.Mappers;
using DotNetTest.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace DotNetTestTests.Tests.ControllersTests
{
    [TestFixture]
    public class PersonControllerTests
    {
        [Test]
        public void GetById_Returns_OkResult_With_PersonResponse()
        {
            // Arrange
            const int personId = 1;
            var mockLogger = new Mock<ILogger<PersonController>>();
            var mockPersonService = new Mock<IPersonService>();
            var mockPersonResponseMapper = new PersonResponseMapper(mockPersonService.Object); // Needs instance, not a mock
            var mockCreateCommandMapper = Mock.Of<CreatePersonCommandMapper>();
            var controller = new PersonController(
                mockLogger.Object,
                mockPersonService.Object,
                mockPersonResponseMapper,
                mockCreateCommandMapper
            ); // I don't like this

            var mockPerson = new Person { Id = personId, Name = "John", Surname = "Doe" };

            mockPersonService.Setup(service => service.GetById(personId)).Returns(mockPerson);
            
            var personResponse = mockPersonResponseMapper.Map(mockPerson);
            personResponse.AmountConsumed = 10;
            
            var result = controller.GetById(personId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = (OkObjectResult)result;

            var expectedPersonResponse = personResponse;
            var actualPersonResponse = okResult.Value as PersonResponse;

            Assert.That(actualPersonResponse, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(actualPersonResponse.Id, Is.EqualTo(expectedPersonResponse.Id));
                Assert.That(actualPersonResponse.Name, Is.EqualTo(expectedPersonResponse.Name));
                Assert.That(actualPersonResponse.Surname, Is.EqualTo(expectedPersonResponse.Surname));
            }); //could get messy with larger entities
        }

        [Test]
        public void GetById_Returns_NotFound_When_Person_Not_Found()
        {
            // Arrange
            const int personId = 1;
            var mockLogger = new Mock<ILogger<PersonController>>();
            var mockPersonService = new Mock<IPersonService>();
            var mockPersonResponseMapper = new Mock<PersonResponseMapper>(mockPersonService.Object);
            var mockCreateCommandMapper = Mock.Of<CreatePersonCommandMapper>();
            
            mockPersonService.Setup(service => service.GetById(personId)).Returns((Person)null);
            
            var controller = new PersonController(
                mockLogger.Object,
                mockPersonService.Object,
                mockPersonResponseMapper.Object,
                mockCreateCommandMapper
            );

            // Act
            var result = controller.GetById(personId);

            // Assert
            Assert.That(result, Is.InstanceOf<NotFoundResult>());
        }

    }
}