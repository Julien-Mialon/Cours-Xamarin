using Storm.Api.Core.CQRS;
using TimeTracker.Api.Domains.Authentications.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Authentications;

public static class AuthenticationBootstrapper
{
	public static IServiceCollection UseAuthenticationModule(this IServiceCollection services)
	{
		services.AddScoped<IAuthenticationService, AuthenticationService>()
			.AddScoped<ICredentialAuthenticationService, CredentialAuthenticationService>()
			.AddScoped<IActionAuthenticator<User, object>, ActionTokenAuthenticator>();
			
		return services;
	}
}