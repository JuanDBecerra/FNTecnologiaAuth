using Newtonsoft.Json;
using Prueba.Domain.Entities.Dtos;
using Prueba.Domain.Entities.Request;
using Prueba.Domain.Entities.Response;
using Prueba.IntegralTest;
using System.Net;
using System.Text;

namespace Prueba.Tests.Integration.FullIntegration
{
    public class AuthControllerFullIntegrationTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly CustomWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public AuthControllerFullIntegrationTests(CustomWebApplicationFactory factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task Create_InvalidName_ReturnsBadRequest()
        {
            var createUserRequest = new CreateUserRequest
            {
                DocumentNumber = "1234",
                Password = "StrongPassword123!",
                Name = "invalid-name",
                LastName = "Test",
                Rol = 1
            };

            var json = JsonConvert.SerializeObject(createUserRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorResponse = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse);
            Assert.NotNull(error.Error);
        }       

        [Fact]
        public async Task Login_NonExistentUser_ReturnsBadRequest()
        {
            var loginRequest = new AuthRequest();

            var loginJson = JsonConvert.SerializeObject(loginRequest);
            var loginContent = new StringContent(loginJson, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", loginContent);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var errorResponse = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse);
            Assert.NotNull(error.Error);
        }

        [Fact]
        public async Task Create_EmptyRequestBody_ReturnsBadRequest()
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Login_EmptyRequestBody_ReturnsBadRequest()
        {
            var content = new StringContent("", Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth/login", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Create_MissingRequiredFields_ReturnsBadRequest()
        {
            var createUserRequest = new CreateUserRequest
            {
                Name = "missing@test.com"
            };

            var json = JsonConvert.SerializeObject(createUserRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/auth", content);

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
