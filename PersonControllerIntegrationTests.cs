using System.Net;
using System.Text;
using DotNetTest;
using DotNetTest.Controllers.Responses;
using DotNetTest.Entities;
using DotNetTest.Repositories.Requests;
using DotNetTest.Services;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using Newtonsoft.Json;

namespace DotNetTestTests.Tests.IntegrationTests
{
    [TestFixture]
    public class PersonControllerIntegrationTests
    {
        private HttpClient _httpClient;
        private Mock<IPersonService> _personServiceMock;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _personServiceMock = new Mock<IPersonService>();

            var webApplicationFactory = new WebApplicationFactory<Startup>();
            _httpClient = webApplicationFactory.CreateClient();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _httpClient.Dispose();
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public async Task GetById_ExistingId_ReturnsOk(int id)
        {
            // Arrange
            var personResponse = new Person
            {
                Id = id,
                Name = "John",
                Surname = "Doe",
                Username = "johndoe"
            };

            _personServiceMock.Setup(service => service.GetById(id)).Returns(personResponse);

            // Act
            var response = await _httpClient.GetAsync($"/persons/{id}");
            var responseBody = await response.Content.ReadAsStringAsync();

            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(responseBody, Is.Not.Null.And.Not.Empty);
            });
            var returnedPersonResponse = JsonConvert.DeserializeObject<PersonResponse>(responseBody);
            Assert.That(returnedPersonResponse, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(returnedPersonResponse.Id, Is.EqualTo(personResponse.Id));
                Assert.That(returnedPersonResponse.Name, Is.EqualTo(personResponse.Name));
                Assert.That(returnedPersonResponse.Surname, Is.EqualTo(personResponse.Surname));
                Assert.That(returnedPersonResponse.Username, Is.EqualTo(personResponse.Username));
            });
            _personServiceMock.Verify(service => service.GetById(id), Times.Once);
        }

        [TestCase(1, 1, 10)]
        [TestCase(2, 2, 5)]
        [TestCase(3, 1, 20)]
        public async Task GetAll_ValidParameters_ReturnsOk(int page, int pageSize, int expectedCount)
        {
            // Arrange
            var personPagedResponse = new PersonsPagedRequest
            {
                Persons = GeneratePersons(expectedCount)
            };

            _personServiceMock.Setup(service => service.GetAll(page, pageSize)).Returns(personPagedResponse);

            // Act
            var response = await _httpClient.GetAsync($"/persons?page={page}&pageSize={pageSize}");
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Multiple(() =>
            {
                Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(responseBody, Is.Not.Null.And.Not.Empty);
            });
            var returnedPersonPagedResponse = JsonConvert.DeserializeObject<PersonPagedResponse>(responseBody);
            Assert.That(returnedPersonPagedResponse, Is.Not.Null);
            Assert.That(returnedPersonPagedResponse.Persons, Is.Not.Null.And.Not.Empty);
            Assert.That(returnedPersonPagedResponse.Persons.Count, Is.EqualTo(expectedCount));

            _personServiceMock.Verify(service => service.GetAll(page, pageSize), Times.Once);
        }

        [TestCase("Test1", "User1", "thefirst")]
        [TestCase("Test2", "User2", "thesecond")]
        public async Task Create_ValidCommand_ReturnsCreatedAtAction(string firstName, string lastName, string username)
        {
            // Arrange
            var command = new Person
            {
                Name = firstName,
                Surname = lastName,
                Username = username
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            _personServiceMock.Setup(service => service.Create(command));

            // Act
            var response = await _httpClient.PostAsync("/persons/create", requestContent);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            _personServiceMock.Verify(service => service.Create(command), Times.Once);
        }

        [TestCase(1, "John", "johndoe")]
        [TestCase(2, "Jane Smith", "janesmith")]
        public async Task Update_ExistingIdAndValidCommand_ReturnsOk(int id, string name, string username)
        {
            // Arrange
            var command = new Person
            {
                Name = name,
                Username = username
            };

            var requestContent = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

            _personServiceMock.Setup(service => service.Update(command));

            // Act
            var response = await _httpClient.PutAsync($"/persons/{id}", requestContent);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            _personServiceMock.Verify(service => service.Update(command), Times.Once);
        }

        private IEnumerable<Person> GeneratePersons(int count)
        {
            var persons = new List<Person>();
            for (var i = 1; i <= count; i++)
            {
                persons.Add(CreatePersonResponse($"Person {i}", $"Surname {i}", $"username{i}"));
            }
            return persons;
        }

        private Person CreatePersonResponse(string firstName, string lastName, string username)
        {
            return new Person
            {
                Id = 1,
                Name = firstName,
                Surname = lastName,
                Username = username
            };
        }
    }
}
