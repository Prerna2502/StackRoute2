using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using UserService;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserService.Models;
using System.Net.Http.Formatting;
using System.Net;

namespace Test.controller
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class UserControllerTest:IClassFixture<UserWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public UserControllerTest(UserWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByIdShouldReturnUser()
        {
            // The endpoint or route of the controller action.
            string userId = "Mukesh";
            var httpResponse = await _client.GetAsync($"/api/user/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            Assert.Equal("Mukesh", user.Name);
        }

        [Fact, TestPriority(2)]
        public async Task RegisterUserShouldSuccess()
        {
            User user = new User { UserId = "Sam", Name = "Sam Gomes", Contact = "9876543210" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<User>("/api/user", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var _user = JsonConvert.DeserializeObject<User>(stringResponse);

            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.NotNull(_user);
            Assert.Equal("Sam Gomes", _user.Name);
        }


        [Fact, TestPriority(3)]
        public async Task UpdateUserShouldSuccess()
        {
            string userId = "Mukesh";
            User user = new User { UserId = "Mukesh", Contact = "1234567890", Name = "Mukesh Goel" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<User>($"/api/user/{userId}", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var _user = JsonConvert.DeserializeObject<User>(stringResponse);

            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.NotNull(_user);
            Assert.Equal("Mukesh Goel", _user.Name);
        }

        [Fact, TestPriority(4)]
        public async Task DeleteUserShouldSuccess()
        {
            string userId = "Sam";
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/user/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        #endregion positivetests

        #region negativetests
        [Fact, TestPriority(5)]
        public async Task GetByIdShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            string userId = "ABC";
            var httpResponse = await _client.GetAsync($"/api/user/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This user id does not exist", stringResponse);
        }

        [Fact, TestPriority(6)]
        public async Task RegisterUserShouldFail()
        {
            User user = new User { UserId = "Mukesh", Name = "Mukesh Goel", Contact = "9876543210" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<User>("/api/user", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.Conflict, httpResponse.StatusCode);
            Assert.Equal($"This user id already exists", stringResponse);
        }

        
        [Fact, TestPriority(7)]
        public async Task UpdateUserShouldFail()
        {
            string userId = "Sam";
            User user = new User { UserId = "Sam", Contact = "1234567890", Name = "Sam Kaul" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<User>($"/api/user/{userId}", user, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This user id does not exist", stringResponse);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteUserShouldFail()
        {
            string userId = "Sam";
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/user/{userId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This user id does not exist", stringResponse);
        }

        #endregion negativetests
    }
}
