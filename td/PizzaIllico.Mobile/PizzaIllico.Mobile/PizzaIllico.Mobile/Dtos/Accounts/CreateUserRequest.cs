using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Accounts
{
    public class CreateUserRequest
    {
	    [JsonProperty("client_id")]
	    public string ClientId { get; set; }
		
	    [JsonProperty("client_secret")]
	    public string ClientSecret { get; set; }
	    
        [JsonProperty("email")]
        public string Email { get; set; }
		
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
		
        [JsonProperty("last_name")]
        public string LastName { get; set; }
		
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
		
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}