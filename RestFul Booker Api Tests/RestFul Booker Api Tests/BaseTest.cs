
using Microsoft.Playwright;

namespace RestFul_Booker_Api_Tests
{
    public class BaseTest : IAsyncLifetime
    {
        public IPlaywright playwright;
        public IAPIRequestContext request;
        public async Task InitializeAsync()
        {
            playwright = await Playwright.CreateAsync();
            request = await playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = "https://restful-booker.herokuapp.com"
            });
        }
        public async Task DisposeAsync()
        {
            await request.DisposeAsync();
        }

    }
}