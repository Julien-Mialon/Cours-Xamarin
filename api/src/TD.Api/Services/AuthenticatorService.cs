using System;
using System.Net;
using System.Threading.Tasks;
using Common.Api.Extensions;
using Common.Core.CQRS;
using Common.Core.Database;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using Common.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack.OrmLite;
using TD.Api.Models;

namespace TD.Api.Services
{
	public class AuthenticatorService : ICommandAuthenticator<User>
	{
		private const string HEADER = "Authorization";
		private const string TYPE = "Bearer";
		
		private readonly IHttpContextAccessor _accessor;

		public AuthenticatorService(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}

		public async Task<(bool authenticated, User user, string token)> Authenticate(Scopes scope)
		{
			var connection = await _accessor.HttpContext.RequestServices.GetService<IDatabaseService>().Connection;

			var token = _accessor.GetAuthenticationToken(HEADER, TYPE, HEADER);

			var authenticationToken = await connection.From<AuthenticationToken>()
				.Where(x => x.AccessToken == token)
				.NotDeleted()
				.AsSingleAsync(connection);

			if (authenticationToken is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Unauthorized, "invalid token");
			}

			if (authenticationToken.ExpirationDate < DateTime.UtcNow)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Unauthorized, "expired token");
			}

			User user = await connection.From<User>()
				.Where(x => x.Id == authenticationToken.UserId)
				.NotDeleted()
				.AsSingleAsync(connection);

			if (user is null)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Forbidden, "user account has been deleted");
			}

			return (true, user, token);
		}
	}
}