using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Dtos
{
	public class PaginatedQueryParameters
	{
		[FromQuery]
		public int? Page { get; set; }
		
		[FromQuery]
		public int? Count { get; set; }
	}
}