using ServiceStack.OrmLite;
using Storm.Api.Core.Extensions;
using Storm.Api.Core.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Authentications.Services;

public interface ICredentialAuthenticationService
{
	Task<IAuthentication> LoginWithCredentials(string login, string password);
	Task<LoginPasswordUserAuthentication> GetAuthenticationForUser(User user);
	Task<LoginPasswordUserAuthentication> CreateAuthenticationForUser(User user, string password);
	Task UpdateAuthentication(LoginPasswordUserAuthentication authentication);
	Task UpdatePassword(LoginPasswordUserAuthentication authentication, string password);

}

public class CredentialAuthenticationService : BaseDatabaseService, ICredentialAuthenticationService
{
	public CredentialAuthenticationService(IServiceProvider services) : base(services)
	{
	}

	public async Task<IAuthentication> LoginWithCredentials(string login, string password)
	{
		return await UseConnection(async connection =>
		{
			LoginPasswordUserAuthentication authentication = await connection.From<LoginPasswordUserAuthentication>()
				.NotDeleted()
				.Where(x => x.Login == login)
				.AsSingleAsync(connection);

			if (authentication is null)
			{
				return null;
			}

			if (authentication.Password == password.AsSha256())
			{
				return authentication;
			}

			return null;
		});
	}

	public async Task<LoginPasswordUserAuthentication> GetAuthenticationForUser(User user)
	{
		return await UseConnection(async connection =>
		{
			return await connection.From<LoginPasswordUserAuthentication>()
				.NotDeleted()
				.Where(x => x.UserId == user.Id)
				.AsSingleAsync(connection);
		});
	}

	public async Task<LoginPasswordUserAuthentication> CreateAuthenticationForUser(User user, string password)
	{
		return await UseConnection(async connection =>
		{
			LoginPasswordUserAuthentication authentication = new LoginPasswordUserAuthentication
			{
				CollationId = Guid.NewGuid(),
				UserId = user.Id,
				Login = user.Email,
				Password = password.AsSha256(),
			};

			authentication.Id = await connection.InsertAsync(authentication, selectIdentity: true);

			return authentication;
		});
	}

	public async Task UpdateAuthentication(LoginPasswordUserAuthentication authentication)
	{
		await UseConnection(async connection =>
		{
			await connection.UpdateAsync(authentication);
		});
	}

	public async Task UpdatePassword(LoginPasswordUserAuthentication authentication, string password)
	{
		await UseConnection(async connection =>
		{
			authentication.Password = password.AsSha256();

			await connection.UpdateAsync(authentication);
		});
	}
}