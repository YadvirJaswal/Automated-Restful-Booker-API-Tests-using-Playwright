using Microsoft.Playwright;
using RestFul_Booker_Api_Tests.Models;

namespace RestFul_Booker_Api_Tests.Tests
{
    public class BookingTests : BaseTest
    {

        [Fact]
        public async Task CreateBooking()
        {
            //  Arrange : Define Request Payload
            var newBooking = new BookingModel
            {
                FirstName = "John",
                LastName = "Deo",
                TotalPrice = 100,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = new DateOnly(2025, 2, 10),
                    CheckOut = new DateOnly(2025, 2, 15)
                },
                AdditionalNeeds = "BreakFast"
            };

            // Act : Send Post request
            var postResponse = await request.PostAsync("/booking", new APIRequestContextOptions
            {
                DataObject = newBooking,
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            });

            Assert.Equal(200, postResponse.Status);
            var responseBody = await postResponse.JsonAsync();
            Assert.Equal("John", responseBody?.GetProperty("booking").GetProperty("firstname").GetString());
            Assert.True(responseBody?.GetProperty("booking").GetProperty("depositpaid").GetBoolean());
        }
        [Fact]
        public async Task GetBookingUsingId()
        {
            var id = 1;
            var getResponse = await request.GetAsync($"/booking/{id}");
            Assert.Equal(200, getResponse.Status);

            var responseBody = await getResponse.JsonAsync();
            Assert.Equal("Mary", responseBody?.GetProperty("firstname").GetString());
            Assert.False(responseBody?.GetProperty("depositpaid").GetBoolean());
        }
    }
}
