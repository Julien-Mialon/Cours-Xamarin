using Storm.Api.Core.CQRS;
using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Authentications.Converters;
using TimeTracker.Api.Domains.Authentications.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Authentications;

namespace TimeTracker.Api.Domains.Authentications.Actions;

public class RefreshTokenCommandParameter
{
	public string RefreshToken { get; set; }
		
	public string ClientId { get; set; }
		
	public string ClientSecret { get; set; }
}
	
public class RefreshTokenCommand : BaseAction<RefreshTokenCommandParameter, LoginResponse>
{
	public RefreshTokenCommand(IServiceProvider services) : base(services)
	{
	}

	protected override async Task<LoginResponse> Action(RefreshTokenCommandParameter parameter)
	{
		IAuthenticationService authenticationService = Resolve<IAuthenticationService>();
		ApiClient client = await authenticationService.FindClient(parameter.ClientId, parameter.ClientSecret);
		client.UnauthorizedIfNull();

		AuthenticationToken token = await authenticationService.GetTokenByRefresh(parameter.RefreshToken, client);
		token.UnauthorizedIfNull();

		AuthenticationToken newToken = await authenticationService.RefreshToken(token);

		return newToken.ToDtos();
	}
}