using Microsoft.AspNetCore.Builder;

namespace Common.Api.Middleware
{
	public static class HttpsMiddleware
	{
		public static IApplicationBuilder EnforceHttps(this IApplicationBuilder app)
		{
			if (!EnvironmentHelper.IsHttpsRequired)
			{
				return app;
			}

			return app.Use(async (context, next) =>
			{
				if (context.Request.IsHttps)
				{
					await next();
				}
				else
				{
					context.Response.Redirect($"https://{context.Request.Host}{context.Request.Path}", true);
				}
			});
		}
	}
}