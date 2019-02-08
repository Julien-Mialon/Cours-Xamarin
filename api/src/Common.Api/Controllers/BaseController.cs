using System;
using System.Threading.Tasks;
using Common.Api.Dtos;
using Common.Core.CQRS;
using Common.Core.Exceptions;
using Common.Core.Extensions;
using Common.Core.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace Common.Api.Controllers
{
	public abstract class BaseController : Controller
	{
		/// <summary>
		/// Service collection to resolve every service from
		/// </summary>
		protected IServiceProvider Services { get; }

		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="services">Injected service collection from AspNet.Core</param>
		protected BaseController(IServiceProvider services)
		{
			Services = services;
		}

		protected Task<IActionResult> WrapForError<T>(Func<Task<T>> executor)
		{
			return WrapForErrorRaw(async () =>
			{
				T result = await executor();

				if (result is Response response)
				{
					response.IsSuccess = true;
					return Ok(response);
				}
				
				return Ok(new Response<T>
				{
					Data = result,
					IsSuccess = true,
				});
			});
		}

		protected async Task<IActionResult> WrapForErrorRaw(Func<Task<IActionResult>> executor)
		{
			try
			{
				return await executor();
			}
			catch (DomainException ex)
			{
				return Ok(new Response
				{
					IsSuccess = false,
					ErrorCode = ex.ErrorCode,
					ErrorMessage = ex.ErrorMessage
				});
			}
			catch (DomainHttpCodeException ex)
			{
				return StatusCode(ex.Code, new Response
				{
					IsSuccess = false,
					ErrorCode = "GENERIC_HTTP_ERROR",
					ErrorMessage = ex.Message
				});
			}
			catch (Exception ex)
			{
				if (!EnvironmentHelper.IsAvailableClient)
				{
					throw;
				}

				return StatusCode(500, new Response
				{
					IsSuccess = false,
					ErrorCode = "GENERIC_HTTP_ERROR",
					ErrorMessage = $"Exception: {ex}"
				});
			}
		}

		/// <summary>
		/// Execute a command on desired parameters
		/// </summary>
		/// <typeparam name="TCommand"></typeparam>
		/// <typeparam name="TParameter"></typeparam>
		/// <typeparam name="TOutput"></typeparam>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected Task<IActionResult> Command<TCommand, TParameter, TOutput>(TParameter parameter)
			where TCommand : ICommand<TParameter, TOutput>
		{
			return WrapForError(() => Services.ExecuteCommand<TCommand, TParameter, TOutput>(parameter));
		}

		/// <summary>
		/// Execute a query with parameters
		/// </summary>
		/// <typeparam name="TQuery"></typeparam>
		/// <typeparam name="TParameter"></typeparam>
		/// <typeparam name="TOutput"></typeparam>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected Task<IActionResult> Query<TQuery, TParameter, TOutput>(TParameter parameter)
			where TQuery : IQuery<TParameter, TOutput>
		{
			return WrapForError(() => Services.ExecuteQuery<TQuery, TParameter, TOutput>(parameter));
		}
		
		/// <summary>
		/// Execute a query with parameters
		/// </summary>
		/// <typeparam name="TQuery"></typeparam>
		/// <typeparam name="TParameter"></typeparam>
		/// <typeparam name="TOutput"></typeparam>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected Task<IActionResult> QueryRaw<TQuery, TParameter, TOutput>(TParameter parameter)
			where TQuery : IQuery<TParameter, TOutput>
		{
			return WrapForError(() => Services.ExecuteQuery<TQuery, TParameter, TOutput>(parameter));
		}
		
		/// <summary>
		/// Execute a query with parameters
		/// </summary>
		/// <typeparam name="TQuery"></typeparam>
		/// <typeparam name="TParameter"></typeparam>
		/// <param name="parameter"></param>
		/// <returns></returns>
		protected Task<IActionResult> FileQuery<TQuery, TParameter>(TParameter parameter)
			where TQuery : IQuery<TParameter, FileQueryResult>
		{
			return WrapForErrorRaw(async () =>
			{
				FileQueryResult result = await Services.ExecuteQuery<TQuery, TParameter, FileQueryResult>(parameter);
				if (result.FileName.IsNullOrEmpty())
				{
					return File(result.Data, result.ContentType);
				}

				return File(result.Data, result.ContentType, result.FileName);
			});
		}
	}
}