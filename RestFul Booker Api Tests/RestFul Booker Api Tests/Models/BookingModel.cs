using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestFul_Booker_Api_Tests.Models
{
    public class BookingModel
    {
        [JsonPropertyName("firstname")]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        public string LastName { get; set; }

        [JsonPropertyName("totalprice")]
        public int TotalPrice { get; set; }

        [JsonPropertyName("depositpaid")]
        public bool DepositPaid { get; set; }

        [JsonPropertyName("bookingdates")]
        public BookingDates BookingDates { get; set; }

        [JsonPropertyName("additionalneeds")]
        public string AdditionalNeeds { get; set; }

        public BookingModel()
        {
            // Default constructor to initialize BookingDates
            BookingDates = new BookingDates();
        }
    }
    public class BookingDates
    {
        [JsonPropertyName("checkin")]
        public DateOnly CheckIn { get; set; }

        [JsonPropertyName("checkout")]
        public DateOnly CheckOut { get; set; }
    }
}
