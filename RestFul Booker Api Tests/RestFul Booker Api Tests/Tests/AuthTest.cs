using Microsoft.Playwright;
using RestFul_Booker_Api_Tests.Models;

namespace RestFul_Booker_Api_Tests.Tests
{
    public class AuthTest:BaseTest
    {
        // Get an authentication token

        [Fact]
        public async Task GetAuthToken()
        {
            // Arrange : Define request payload
            var payload = new AuthModel
            {
                UserName = "admin",
                Password = "password123"
            };

            // Act : Send Post Request
            var postResponse = await request.PostAsync("/auth", new APIRequestContextOptions
            {
                DataObject = payload
            });

            // Assert : Validate the Status Code
            Assert.Equal(200,postResponse.Status);

            // Get the response
            var responseBody = await postResponse.JsonAsync();
            Assert.NotNull(responseBody?.GetProperty("token").GetString());
        }
    }
}
