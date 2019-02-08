using Common.Core.Database;
using Microsoft.Extensions.Configuration;

namespace Common.Api.Configurations
{
	public static class DatabaseConfigurationHelper
	{
		public static DatabaseConfigurationBuilder LoadDatabaseConfiguration(this IConfiguration configuration)
		{
			//configuration keys : "Name", "User", "Password", "Host"
			
			return new DatabaseConfigurationBuilder()
				.UseMysql(
					configuration["Host"],
					configuration["Name"],
					configuration["User"],
					configuration["Password"]
				);
		}
	}
}