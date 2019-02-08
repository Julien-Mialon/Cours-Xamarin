using Newtonsoft.Json;

namespace Common.Api.Dtos
{
	public class Response
	{
		[JsonProperty("is_success")]
		public bool IsSuccess { get; set; }
		
		[JsonProperty("error_code")]
		public string ErrorCode { get; set; }
		
		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }
	}

	public class Response<T> : Response
	{
		[JsonProperty("data")]
		public T Data { get; set; }
	}
}