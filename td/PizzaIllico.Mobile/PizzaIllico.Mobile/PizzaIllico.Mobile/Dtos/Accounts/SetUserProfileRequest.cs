using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Accounts
{
	public class SetUserProfileRequest
	{
		[JsonProperty("email")]
		public string Email { get; set; }
		
		[JsonProperty("first_name")]
		public string FirstName { get; set; }
		
		[JsonProperty("last_name")]
		public string LastName { get; set; }
		
		[JsonProperty("phone_number")]
		public string PhoneNumber { get; set; }
	}
}