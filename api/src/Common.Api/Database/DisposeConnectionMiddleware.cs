using Common.Core.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Database
{
	public static class DisposeConnectionMiddleware
	{
		public static IApplicationBuilder UseDisposeConnectionMiddleware(this IApplicationBuilder app)
		{
			return app.Use(async (context, next) =>
			{
				await next();
				
				context.RequestServices
					.GetService<IDatabaseService>()
					.Dispose();
			});
		}
	}
}