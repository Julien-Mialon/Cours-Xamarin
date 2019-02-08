using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Exceptions;
using Common.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.CQRS
{
	public abstract class BaseAuthenticatedQuery<TParameter, TOutput, TUser> : BaseQuery<TParameter, TOutput> where TUser : ICommandUser
	{
		private readonly Scopes _scope;
		private readonly ICommandAuthenticator<TUser> _authenticator;
		
		protected TUser User { get; private set; }
		
		protected string AuthenticationToken { get; private set; }

		protected BaseAuthenticatedQuery(IServiceProvider provider, Scopes scope) : base(provider)
		{
			_scope = scope;
			_authenticator = Services.GetService<ICommandAuthenticator<TUser>>();
		}

		public override async Task<TOutput> Execute(TParameter parameter)
		{
			(bool authenticated, TUser user, string token) = await _authenticator.Authenticate(_scope);
			if (!authenticated)
			{
				throw new DomainHttpCodeException(HttpStatusCode.Unauthorized, "Unauthorized, unable to authenticate");
			}

			User = user;
			AuthenticationToken = token;
			await Authorize(parameter, user);
			
			return await base.Execute(parameter);
		}
		
		protected virtual Task Authorize(TParameter parameter, TUser user)
		{
			return Task.CompletedTask;
		}
	}
}