using Swashbuckle.AspNetCore.Annotations;

namespace Common.Api.Attributes
{
	public class CategoryAttribute : SwaggerOperationAttribute
	{
		public CategoryAttribute(string category) : base(category)
		{
			Tags = new[] { category };
		}
	}
}