using Common.Api.Dtos;
using Common.Core.Parameters;

namespace Common.Api.Converters
{
	public static class ParameterConverters
	{
		public static PaginationParameter ToPaginationParameter(this PaginatedQueryParameters source)
		{
			if (source == null)
			{
				return new PaginationParameter();
			}
			
			return new PaginationParameter
			{
				Count = source.Count,
				Page = source.Page,
			};
		}
	}
}