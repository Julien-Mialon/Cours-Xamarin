using Newtonsoft.Json;

namespace TD.Api.Dtos
{
	public class ImageItem
	{
		[JsonProperty("id")]
		public int Id { get; set; }
	}
}