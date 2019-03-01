using System;
using Common.Core.CQRS;
using Common.Core.Models;
using TD.Api.Models;

namespace TD.Api.Bases
{
	public abstract class BaseTdQuery<TParameter, TResult> : BaseQuery<TParameter, TResult>
	{
		public BaseTdQuery(IServiceProvider provider) : base(provider)
		{
		}
	}

	public abstract class BaseAuthenticatedTdQuery<TParameter, TResult> : BaseAuthenticatedQuery<TParameter, TResult, User>
	{
		protected BaseAuthenticatedTdQuery(IServiceProvider provider) : base(provider, Scopes.Any)
		{
		}
	}
}