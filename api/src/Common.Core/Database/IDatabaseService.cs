using System;
using System.Data;
using System.Threading.Tasks;

namespace Common.Core.Database
{
	public interface IDatabaseService : IDisposable
	{
		Task<IDbConnection> Connection { get; }

		Task<IDatabaseTransaction> Transaction();
	}
}