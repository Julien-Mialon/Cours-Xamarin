using System;
using System.Net;
using System.Threading.Tasks;
using Common.Core.Constants;
using Common.Core.Exceptions;

namespace Common.Core.CQRS
{
	/// <summary>
	/// Base class for each commands
	/// </summary>
	/// <typeparam name="TParameter"></typeparam>
	/// <typeparam name="TOutput"></typeparam>
	public abstract class BaseCommand<TParameter, TOutput> : ICommand<TParameter, TOutput>
	{
		protected IServiceProvider Services { get; }
		
		protected BaseCommand(IServiceProvider services)
		{
			Services = services;
		}
	    
		/// <summary>
		/// Method to execute the command
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		public virtual Task<TOutput> Execute(TParameter parameter)
		{
			if (!ValidateParameter(parameter))
			{
				throw new DomainHttpCodeException(HttpStatusCode.BadRequest, GenericErrors.INVALID_PARAMETERS_MESSAGE);
			}
			PrepareParameters(parameter);
			return Action(parameter);
		}

		/// <summary>
		/// Method to execute the command
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected abstract Task<TOutput> Action(TParameter parameter);

		/// <summary>
		/// Validate input before execution of the command
		/// </summary>
		/// <param name="parameter"></param>
		protected virtual bool ValidateParameter(TParameter parameter)
		{
			return parameter != null;
		}

		/// <summary>
		/// Change some parameters 
		/// </summary>
		/// <param name="parameter"></param>
		protected virtual void PrepareParameters(TParameter parameter)
		{
		    
		}
	}
}