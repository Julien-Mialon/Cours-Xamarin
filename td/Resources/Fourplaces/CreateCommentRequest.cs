using Newtonsoft.Json;

namespace TD.Api.Dtos
{
	public class CreateCommentRequest
	{
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}