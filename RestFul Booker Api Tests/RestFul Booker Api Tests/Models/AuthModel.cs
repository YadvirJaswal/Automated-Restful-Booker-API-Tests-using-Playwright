using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RestFul_Booker_Api_Tests.Models
{
    public class AuthModel
    {
        [JsonPropertyName("username")]
        public string UserName {  get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
