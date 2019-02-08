using System;
using Common.Core.CQRS;
using Common.Core.Models;
using TD.Api.Models;

namespace TD.Api.Bases
{
	public abstract class BaseTdCommand<TParameter, TResult> : BaseCommand<TParameter, TResult>
	{
		public BaseTdCommand(IServiceProvider provider) : base(provider)
		{
		}
	}
}