using System.Data;
using System.Threading.Tasks;
using Common.Core.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Api.Database
{
	public class DatabaseServiceAccessor : IDatabaseServiceAccessor
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public Task<IDbConnection> Connection => _contextAccessor.HttpContext.RequestServices.GetService<IDatabaseService>().Connection;
		
		public DatabaseServiceAccessor(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}
	}
}