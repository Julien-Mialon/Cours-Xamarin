using ServiceStack.OrmLite;
using Storm.Api.Core.Extensions;
using Storm.Api.Core.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Accounts.Services;

public interface IUserService
{
	Task<User> GetUser(long userId);
	Task<User> GetUser(string email);
	Task UpdateUser(User user);
}

public class UserService : BaseDatabaseService, IUserService
{
	public UserService(IServiceProvider services) : base(services)
	{
	}

	public async Task<User> GetUser(long userId)
	{
		return await UseConnection(async connection =>
		{
			return await connection.From<User>()
				.NotDeleted()
				.Where(x => x.Id == userId)
				.AsSingleAsync(connection);
		});
	}

	public async Task<User> GetUser(string email)
	{
		return await UseConnection(async connection =>
		{
			return await connection.From<User>()
				.NotDeleted()
				.Where(x => x.Email == email)
				.AsSingleAsync(connection);
		});
	}

	public async Task UpdateUser(User user)
	{
		await UseConnection(async connection =>
		{
			await connection.UpdateAsync(user);
		});
	}
}