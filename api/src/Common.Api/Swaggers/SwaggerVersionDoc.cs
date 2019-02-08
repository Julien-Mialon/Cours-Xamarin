using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Common.Api.Swaggers
{
	public class SwaggerVersionDoc
	{
		public string ProjectName { get; }
		public string Version { get; }
			
		public List<SwaggerVersionModule> Modules { get; }

		public SwaggerVersionDoc(string projectName, string version, params SwaggerVersionModule[] modules)
		{
			ProjectName = projectName;
			Version = version;
			Modules = modules.ToList();
		}

		public void Apply(SwaggerGenOptions options)
		{
			options.SwaggerDoc(Version, new Info { Title = $"{ProjectName} API", Version = Version });
			foreach (SwaggerVersionModule module in Modules)
			{
				options.SwaggerDoc($"{Version}_{module.ModuleName}", new Info { Title = $"{ProjectName} API - {module.ModuleName}", Version = Version });	
			}
		}

		public void Apply(SwaggerUIOptions options)
		{
			options.SwaggerEndpoint($"/swagger/{Version}/swagger.json", $"{ProjectName} API {Version}");
			foreach (SwaggerVersionModule module in Modules)
			{
				options.SwaggerEndpoint($"/swagger/{Version}_{module.ModuleName}/swagger.json", $"{ProjectName} API {Version} - {module.ModuleName}");	
			}
		}

		public bool InclusionPredicate(string version, ApiDescription description)
		{
			if (version != Version && !version.StartsWith($"{Version}_"))
			{
				return false;
			}
				
			foreach (SwaggerVersionModule module in Modules)
			{
				if (module.MatchExpressions.Any(x => description.RelativePath.Contains(x)))
				{
					return version == $"{Version}_{module.ModuleName}";
				}
			}

			return version == Version;
		}
	}
}