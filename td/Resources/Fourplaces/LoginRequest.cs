using Newtonsoft.Json;

namespace TD.Api.Dtos
{
    public class LoginRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}