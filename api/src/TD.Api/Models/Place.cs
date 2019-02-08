using Common.Core.Models;
using ServiceStack.DataAnnotations;

namespace TD.Api.Models
{
	[Alias("Places")]
	public class Place : BaseEntity
	{
		[PrimaryKey]
		[AutoIncrement]
		public int Id { get; set; }
		
		[StringLength(StringLengthAttribute.MaxText)]
		public string Title { get; set; }
		
		[StringLength(StringLengthAttribute.MaxText)]
		public string Description { get; set; }
		
		[References(typeof(ImageModel))]
		public int ImageId { get; set; }
		
		public double Latitude { get; set; }
		
		public double Longitude { get; set; }
	}
}