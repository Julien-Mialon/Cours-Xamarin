using System.Data;
using System.Threading.Tasks;
using ServiceStack.OrmLite;

namespace Common.Core.Database
{
	public sealed class DatabaseService : IDatabaseService
	{
		private readonly IDatabaseConnectionFactory _factory;
		private readonly object _mutex = new object();
		private bool _connectionOpened;
		private bool _disposed;
		private Task<IDbConnection> _connection;

		public DatabaseService(IDatabaseConnectionFactory factory)
		{
			_factory = factory;
		}

		public Task<IDbConnection> Connection => GetConnection();
		
		public async Task<IDatabaseTransaction> Transaction()
		{
			IDbConnection connection = await Connection;
			IDbTransaction transaction = connection.OpenTransaction();
			
			return new DatabaseTransaction(transaction);
		}

		private Task<IDbConnection> GetConnection()
		{
			if (_connectionOpened)
			{
				return _connection;
			}

			lock (_mutex)
			{
				if (_connectionOpened)
				{
					return _connection;
				}
				
				_connection = _factory.Open();
				_connectionOpened = true;
				return _connection;
			}
		}

		public void Dispose()
		{
			if (_disposed)
			{
				return;
			}

			if (_connectionOpened)
			{
				_connection.Result.Dispose();
			}
			_disposed = true;
		}
	}
}