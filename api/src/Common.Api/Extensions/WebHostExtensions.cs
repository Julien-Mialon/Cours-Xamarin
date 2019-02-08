using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Common.Api.Extensions
{
	public static class WebHostExtensions
	{
		public static IWebHostBuilder UseCommonConfiguration(this IWebHostBuilder host)
		{
			return host.ConfigureAppConfiguration((context, configurationBuilder) =>
				{
					string name = context.HostingEnvironment.EnvironmentName;
					string environmentTypePart = name;
					int delimiterIndex = environmentTypePart.LastIndexOf('-');
					if (delimiterIndex >= 0)
					{
						environmentTypePart = environmentTypePart.Substring(delimiterIndex + 1);
					}

					switch (environmentTypePart)
					{
						case "dev":
							EnvironmentHelper.Set(EnvironmentHelper.EnvironmentSlot.Dev);
							break;
						case "test":
							EnvironmentHelper.Set(EnvironmentHelper.EnvironmentSlot.Test);
							break;
						case "beta":
							EnvironmentHelper.Set(EnvironmentHelper.EnvironmentSlot.Beta);
							break;
						case "prod":
							EnvironmentHelper.Set(EnvironmentHelper.EnvironmentSlot.Prod);
							break;
						default:
							name = "local";
							EnvironmentHelper.Set(EnvironmentHelper.EnvironmentSlot.Local);
							break;
					}

					configurationBuilder
						.AddJsonFile($"appsettings.{name}.json", optional: false, reloadOnChange: true)
						.AddEnvironmentVariables();
				})
				.UseKestrel(x =>
				{
					x.Limits.MaxRequestBodySize = 2_000_000_000;
					x.Limits.MaxRequestLineSize = 10_000_000;
					x.Limits.MaxRequestBufferSize = 10_000_000;
					x.Limits.MaxRequestHeadersTotalSize = 10_000_000;
				});
		}
	}
}