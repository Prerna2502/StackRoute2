using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using CategoryService;
using CategoryService.Models;
using Newtonsoft.Json;
using Xunit;

namespace Test.controller
{
    [TestCaseOrderer("Test.PriorityOrderer", "Test")]
    public class CategoryControllerTest:IClassFixture<CategoryWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        public CategoryControllerTest(CategoryWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        #region positivetests
        [Fact, TestPriority(1)]
        public async Task GetByUserIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            string userId = "Mukesh";
            var httpResponse = await _client.GetAsync($"/api/category/{userId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<IEnumerable<Category>>(stringResponse);
            Assert.Contains(category, c => c.Name == "Sports");
        }

        [Fact, TestPriority(2)]
        public async Task GetByIdShouldSuccess()
        {
            // The endpoint or route of the controller action.
            int categoryId = 101;
            var httpResponse = await _client.GetAsync($"/api/category/{categoryId}");

            // Must be successful.
            httpResponse.EnsureSuccessStatusCode();

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<Category>(stringResponse);
            Assert.Equal("Sports", category.Name);
        }

        [Fact, TestPriority(2)]
        public async Task CreateCategoryShouldSuccess()
        {
            Category category = new Category { Name = "API", Description = "Microservice", CreatedBy = "Mukesh" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PostAsync<Category>("/api/category", category, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject<Category>(stringResponse);
            Assert.Equal(HttpStatusCode.Created, httpResponse.StatusCode);
            Assert.IsAssignableFrom<Category>(response);
        }

        [Fact, TestPriority(4)]
        public async Task UpdateCategoryShouldSuccess()
        {
            int categoryId = 101;
            Category category = new Category { Id = 101, Name = "Testing", Description = "Integration Testing", CreatedBy = "Mukesh" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Category>($"/api/category/{categoryId}", category, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }

        [Fact, TestPriority(5)]
        public async Task DeleteCategoryShouldSuccess()
        {
            int categoryId = 103;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/category/{categoryId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.OK, httpResponse.StatusCode);
            Assert.True(Convert.ToBoolean(stringResponse));
        }
        #endregion positivetests

        #region negativetests
        [Fact, TestPriority(6)]
        public async Task GetByIdShouldReturnNotFound()
        {
            // The endpoint or route of the controller action.
            int categoryId = 103;
            var httpResponse = await _client.GetAsync($"/api/category/{categoryId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This category id not found", stringResponse);
        }

        [Fact, TestPriority(7)]
        public async Task UpdateCategoryShouldFail()
        {
            int categoryId = 103;
            Category category = new Category { Id = 103, Name = "API", Description = "Microservice", CreatedBy = "Mukesh" };
            HttpRequestMessage request = new HttpRequestMessage();
            MediaTypeFormatter formatter = new JsonMediaTypeFormatter();

            // The endpoint or route of the controller action.
            var httpResponse = await _client.PutAsync<Category>($"/api/category/{categoryId}", category, formatter);

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This category id not found", stringResponse);
        }

        [Fact, TestPriority(8)]
        public async Task DeleteCategoryShouldFail()
        {
            int categoryId = 104;
            // The endpoint or route of the controller action.
            var httpResponse = await _client.DeleteAsync($"/api/category/{categoryId}");

            // Deserialize and examine results.
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.Equal(HttpStatusCode.NotFound, httpResponse.StatusCode);
            Assert.Equal($"This category id not found", stringResponse);
        }

        #endregion negativetests
    }
}
