using ServiceStack.OrmLite;
using Storm.Api.Core.Extensions;
using Storm.Api.Core.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Authentications.Services;

public interface IAuthenticationService
{
	Task<ApiClient> FindClient(string clientId, string clientSecret);
	Task<AuthenticationToken> CreateToken(ApiClient client, IAuthentication authentication);
	Task<AuthenticationToken> RefreshToken(AuthenticationToken token);
	Task<(AuthenticationToken token, User user)> GetTokenByAccess(string accessToken);
	Task<AuthenticationToken> GetTokenByRefresh(string refreshToken, ApiClient client);
}

public class AuthenticationService : BaseDatabaseService, IAuthenticationService
{
	public AuthenticationService(IServiceProvider services) : base(services)
	{
	}

	public async Task<ApiClient> FindClient(string clientId, string clientSecret)
	{
		return await UseConnection(async connection =>
		{
			return await connection.From<ApiClient>()
				.NotDeleted()
				.Where(x => x.ClientId == clientId && x.ClientSecret == clientSecret)
				.AsSingleAsync(connection);
		});
	}

	public async Task<AuthenticationToken> CreateToken(ApiClient client, IAuthentication authentication)
	{
		return await UseConnection(async connection =>
		{
			AuthenticationToken token = NewToken(client.Id, authentication.UserId);

			await connection.InsertAsync(token);

			return token;
		});
	}

	public async Task<AuthenticationToken> RefreshToken(AuthenticationToken token)
	{
		return await UseConnection(async connection =>
		{
			AuthenticationToken newToken = NewToken(token.ApiClientId, token.UserId);
			token.IsDeleted = true;
			await connection.InsertAsync(newToken);
			await connection.UpdateAsync(token);

			return newToken;
		});
	}

	public async Task<(AuthenticationToken token, User user)> GetTokenByAccess(string accessToken)
	{
		return await UseConnection(async connection =>
		{
			return await connection.From<AuthenticationToken>()
				.LeftJoin<AuthenticationToken, User>((token, user) => token.UserId == user.Id)
				.NotDeleted()
				.Where(x => x.AccessToken == accessToken)
				.AsSingleMultiAsync<AuthenticationToken, User, (AuthenticationToken, User)>(connection, (token, user) => (token, user));
		});
	}
		
	public async Task<AuthenticationToken> GetTokenByRefresh(string refreshToken, ApiClient client)
	{
		return await UseConnection(async connection =>
		{
			return await connection.From<AuthenticationToken>()
				.NotDeleted()
				.Where(x => x.RefreshToken == refreshToken && x.ApiClientId == client.Id)
				.AsSingleAsync(connection);
		});
	}

	private AuthenticationToken NewToken(long apiClientId, long userId)
	{
		return new AuthenticationToken
		{
			AccessToken = Guid.NewGuid().ToString("N"),
			RefreshToken = Guid.NewGuid().ToString("N"),
			ExpirationDate = DateTime.UtcNow.AddDays(1),
			UserId = userId,
			ApiClientId = apiClientId,
			TokenType = "Bearer",
		};
	}
}