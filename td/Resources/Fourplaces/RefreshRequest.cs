using Newtonsoft.Json;

namespace TD.Api.Dtos
{
    public class RefreshRequest
    {
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}