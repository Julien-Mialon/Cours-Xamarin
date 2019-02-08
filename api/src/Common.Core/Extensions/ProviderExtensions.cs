using System;
using System.Threading.Tasks;
using Common.Core.CQRS;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Extensions
{
	public static class ProviderExtensions
	{
		public static Task<TOutput> ExecuteQuery<TQuery, TParameter, TOutput>(this IServiceProvider services, TParameter parameter) where TQuery : IQuery<TParameter, TOutput>
		{
			TQuery query = ActivatorUtilities.CreateInstance<TQuery>(services);
			return query.Execute(parameter);
		}

		public static Task<TOutput> ExecuteCommand<TCommand, TParameter, TOutput>(this IServiceProvider services,TParameter parameter) where TCommand : ICommand<TParameter, TOutput>
		{
			TCommand command = ActivatorUtilities.CreateInstance<TCommand>(services);
			return command.Execute(parameter);
		}
	}
}