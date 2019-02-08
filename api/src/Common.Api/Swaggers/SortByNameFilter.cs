using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Api.Swaggers
{
	public class SortByNameFilter : IDocumentFilter
	{
		public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
		{
			swaggerDoc.Paths = swaggerDoc.Paths.OrderBy(x => x.Key).ToList().ToDictionary(e => e.Key, e => e.Value);
		}
	}
}