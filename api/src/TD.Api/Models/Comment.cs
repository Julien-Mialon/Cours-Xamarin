using Common.Core.Models;
using ServiceStack.DataAnnotations;

namespace TD.Api.Models
{
	[Alias("Comments")]
	public class Comment : BaseEntity
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Id { get; set; }
		
		[References(typeof(Place))]
		public int PlaceId { get; set; }
		
		public string AuthorName { get; set; }
		
		public string Text { get; set; }
	}
}