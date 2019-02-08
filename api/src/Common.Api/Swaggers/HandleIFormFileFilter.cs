using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Api.Swaggers
{
	public class HandleIFormFileFilter : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			for (int i = 0; i < context.ApiDescription.ParameterDescriptions.Count; i++)
			{
				ApiParameterDescription parameterDescription = context.ApiDescription.ParameterDescriptions[i];
				if (parameterDescription.Type == typeof(IFormFile))
				{
					if (operation.Parameters[i] is NonBodyParameter parameter)
					{
						parameter.In = "formData";
						parameter.Type = "file";
						parameter.Format = "binary";
					}
				}
			}
		}
	}
}