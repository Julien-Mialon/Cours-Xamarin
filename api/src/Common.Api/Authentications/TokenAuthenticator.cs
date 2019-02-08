using System;
using Common.Api.Extensions;
using Common.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Authentications
{
	public class TokenAuthenticator : ITokenAuthenticator
	{
		private readonly IServiceProvider _services;
		private const string HEADER = "X-Token";
		private const string QUERY = "token";

		public TokenAuthenticator(IServiceProvider services)
		{
			_services = services;
		}
		
		public bool Authenticate(string expectedToken)
		{
			return _services.GetService<IHttpContextAccessor>().GetAuthenticationToken(HEADER, QUERY) == expectedToken;
		}
	}
}