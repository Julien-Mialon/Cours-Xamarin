using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Swaggers
{
	public static class CommonSwaggerMiddleware
	{
		public static IServiceCollection AddConfiguredSwagger(this IServiceCollection services, IHostingEnvironment environment, List<SwaggerVersionDoc> documents, List<string> documentationFileNames)
		{
			if (!EnvironmentHelper.IsSwaggerAvailable)
			{
				return services;
			}

			return services.AddSwaggerGen(options =>
			{
				options.DescribeAllEnumsAsStrings();
				options.CustomSchemaIds(x => x.FullName);
				options.DocumentFilter<SortByNameFilter>();
				options.OperationFilter<HandleIFormFileFilter>();
				options.OperationFilter<OperationDescriptionFilter>();

				foreach (SwaggerVersionDoc apiVersionDoc in documents)
				{
					apiVersionDoc.Apply(options);
				}
				
				foreach (string doc in documentationFileNames)
				{
					foreach (string fileDoc in new[]
					{
						$@"{environment.ContentRootPath}{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}netcoreapp2.1{Path.DirectorySeparatorChar}{doc}",
						$@"{environment.ContentRootPath}{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}netcoreapp2.0{Path.DirectorySeparatorChar}{doc}",
						$@"{environment.ContentRootPath}{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}Debug{Path.DirectorySeparatorChar}net461{Path.DirectorySeparatorChar}{doc}",
						$@"{environment.ContentRootPath}{Path.DirectorySeparatorChar}{doc}",
					})
					{
						if (File.Exists(fileDoc))
						{
							options.IncludeXmlComments(fileDoc);
							break;
						}
					}
				}

				options.DocInclusionPredicate((version, apiDescription) => documents.Any(x => x.InclusionPredicate(version, apiDescription)));
			});
		}

		public static IApplicationBuilder UseCommonSwagger(this IApplicationBuilder app, List<SwaggerVersionDoc> documents)
		{
			if (!EnvironmentHelper.IsSwaggerAvailable)
			{
				return app;
			}

			return app.UseSwagger()
				.UseSwaggerUI(options =>
				{
					foreach (SwaggerVersionDoc apiVersionDoc in documents)
					{
						apiVersionDoc.Apply(options);
					}
				});
		}
	}
}