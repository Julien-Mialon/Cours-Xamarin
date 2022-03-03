using TimeTracker.Api.Models;
using TimeTracker.Dtos.Authentications;

namespace TimeTracker.Api.Domains.Authentications.Converters;

public static class LoginConverters
{
	public static LoginResponse ToDtos(this AuthenticationToken authenticationToken)
	{
		return new LoginResponse
		{
			AccessToken = authenticationToken.AccessToken,
			RefreshToken = authenticationToken.RefreshToken,
			ExpiresIn = (int)authenticationToken.ExpirationDate.Subtract(DateTime.UtcNow).TotalSeconds,
			TokenType = authenticationToken.TokenType
		};
	}
}