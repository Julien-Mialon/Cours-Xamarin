using TimeTracker.Api.Models;
using TimeTracker.Dtos.Accounts;

namespace TimeTracker.Api.Domains.Accounts.Converters;

public static class UserProfileConverters
{
	public static UserProfileResponse ToDtos(this User user)
	{
		return new UserProfileResponse
		{
			Email = user.Email,
			FirstName = user.FirstName,
			LastName = user.LastName,
		};
	}
}