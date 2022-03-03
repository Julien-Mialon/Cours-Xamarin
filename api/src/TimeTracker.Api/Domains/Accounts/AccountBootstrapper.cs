using TimeTracker.Api.Domains.Accounts.Services;

namespace TimeTracker.Api.Domains.Accounts;

public static class AccountBootstrapper
{
	public static IServiceCollection UseAccountModule(this IServiceCollection services)
	{
		services.AddScoped<IUserService, UserService>();
			
		return services;
	}
}