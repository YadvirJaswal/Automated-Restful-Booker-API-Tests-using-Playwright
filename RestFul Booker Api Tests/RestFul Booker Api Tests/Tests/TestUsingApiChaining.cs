using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Playwright;
using RestFul_Booker_Api_Tests.Models;

namespace RestFul_Booker_Api_Tests.Tests
{
    public class TestUsingApiChaining : BaseTest
    {
        [Fact]
        public async Task CreateAndGetBooking_ShouldReturnCorrectDetails()
        {
            // Step 1: Generate authentication token
            var responseBody = await GenerateAuthentication();
            var token = responseBody?.GetProperty("token").GetString() ?? "";

            // Step 2: Create a Booking
            var bookingId = await CreateBooking();
            Assert.True(bookingId > 0, "Booking Id should be greater than 0.");

            // Step 3: Use the booking id to get the booking details
            
            var fetchedBooking = await GetBookingById(bookingId);
            Assert.Equal("John", fetchedBooking?.GetProperty("firstname").GetString());

            // Step 4: Update booking
            bool isUpdated = await UpdateBooking(bookingId,token);
            Assert.True(isUpdated, "Booking should be updated successfully");

            // Step 4: Delete the booking
            bool isDeleted = await DeleteBooking(bookingId,token);
            Assert.True(isDeleted, "Booking should be deleted successfully");

           // Step 5: Ensure booking is deleted
            var deletedBooking = await GetBookingById(bookingId);
            Assert.Null(deletedBooking);

        }
        private async Task<JsonElement?> GenerateAuthentication()
        {
            // Arrange : Define request payload
            var payload = new AuthModel
            {
                UserName = "admin",
                Password = "password123"
            };

            // Act : Send Post Request
            var tokenResponse = await request.PostAsync("/auth", new APIRequestContextOptions
            {
                DataObject = payload
            });

            // Assert : Validate the Status Code
            Assert.Equal(200, tokenResponse.Status);

            // Get the response
            var responseBody = await tokenResponse.JsonAsync();
            Assert.NotNull(responseBody?.GetProperty("token").GetString());
            return responseBody;
        }
        private async Task<int> CreateBooking()
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

            var createdBooking = await postResponse.JsonAsync();
            var bookingId = createdBooking?.GetProperty("bookingid").GetInt32() ?? 0;
            return bookingId;
            
        }
        private async Task<JsonElement?> GetBookingById(Int32 bookingId)
        {
            var getResponse = await request.GetAsync($"/booking/{bookingId}");
            if(getResponse.Status!=200) return null;

            var fetchedBooking = await getResponse.JsonAsync();
            return fetchedBooking;
        }
        private async Task<bool> UpdateBooking(int bookingId, string token)
        {
            // Define Request Payload
            var updatedBooking = new BookingModel
            {
                FirstName = "James",
                LastName = "Brown",
                TotalPrice = 100,
                DepositPaid = true,
                BookingDates = new BookingDates
                {
                    CheckIn = new DateOnly(2025, 2, 10),
                    CheckOut = new DateOnly(2025, 2, 15)
                },
                AdditionalNeeds = "BreakFast"
            };
            // Send request
            var putResponse = await request.PutAsync($"/booking/{bookingId}", new APIRequestContextOptions
            {
                DataObject = updatedBooking,
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" } ,
                    {"Cookie",$"token={token}" }
                }
            });

            return putResponse.Status == 200;
        }
        private async Task<bool> DeleteBooking(int bookingId, string token)
        {
            var deleteResponse = await request.DeleteAsync($"/booking/{bookingId}", new APIRequestContextOptions
            {
                Headers = new Dictionary<string, string> { { "Cookie",$"token={token}"} }
            });
            return deleteResponse.Status == 201;
        }
    }
}
