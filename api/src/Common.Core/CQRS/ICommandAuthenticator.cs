using System.Threading.Tasks;
using Common.Core.Models;

namespace Common.Core.CQRS
{
	public interface ICommandAuthenticator<TUser> where TUser : ICommandUser
	{
		Task<(bool authenticated, TUser user, string token)> Authenticate(Scopes scope);
	}
}