using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Accounts.Converters;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Dtos.Accounts;

namespace TimeTracker.Api.Domains.Accounts.Actions;

public class UserProfileQueryParameter
{
		
}
	
public class UserProfileQuery : AuthenticatedAction<UserProfileQueryParameter, UserProfileResponse>
{
	public UserProfileQuery(IServiceProvider services) : base(services)
	{
	}

	protected override Task<UserProfileResponse> Action(UserProfileQueryParameter parameter)
	{
		return Account.ToDtos().AsTask();
	}
}