using Common.Core.Models;
using ServiceStack.DataAnnotations;

namespace TD.Api.Models
{
	[Alias("Images")]
	public class ImageModel : BaseEntity
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Id { get; set; }
		
		public byte[] Data { get; set; }
	}
}