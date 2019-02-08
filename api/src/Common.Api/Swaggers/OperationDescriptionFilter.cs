using System.Collections.Generic;
using System.Linq;
using Common.Api.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Common.Api.Swaggers
{
	public class OperationDescriptionFilter : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			List<object> attributes = context.ApiDescription.ActionAttributes().ToList();
			
			List<string> lines = new List<string>();
			
			lines.AddRange(attributes.OfType<ImplementationNotesAttribute>().Select(x => x.Description));

			List<ErrorCodeAttribute> errorCodes = attributes.OfType<ErrorCodeAttribute>().ToList();
			if (errorCodes.Count > 0)
			{
				if (lines.Count > 0)
				{
					lines.Add("");
				}

				lines.Add("Errors : ");
				
				lines.AddRange(errorCodes.Select(x => $"  - {x.ErrorCode}: {x.Explanation}"));
			}

			operation.Description = string.Join("\r\n", lines);
		}
	}
}