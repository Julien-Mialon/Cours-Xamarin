namespace Common.Core.Services
{
	public interface ITokenAuthenticator
	{
		bool Authenticate(string expectedToken);
	}
}