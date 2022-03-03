namespace TimeTracker.Api.Domains.Authentications.Extensions;

public static class PasswordExtensions
{
	public static bool NotStrongPassword(this string password)
	{
		return password.Length < 6;
	}
}