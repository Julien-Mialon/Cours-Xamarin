using System;
using Newtonsoft.Json;
using TD.Api.Models;

namespace TD.Api.Dtos
{
	public class CommentItem
	{
		[JsonProperty("date")]
		public DateTime Date { get; set; }
		
		[JsonProperty("author")]
		public UserItem Author { get; set; }
		
		[JsonProperty("text")]
		public string Text { get; set; }
	}
}