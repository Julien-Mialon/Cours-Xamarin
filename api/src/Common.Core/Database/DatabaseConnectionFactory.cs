using System.Data;
using System.Threading.Tasks;
using ServiceStack.Data;
using ServiceStack.OrmLite;

namespace Common.Core.Database
{
	/// <inheritdoc cref="IDatabaseConnectionFactory"/>
	internal class DatabaseConnectionFactory : IDatabaseConnectionFactory
	{
		private readonly IDbConnectionFactory _factory;

		/// <summary>
		/// Create a new instance of factory with the underlying connection factory
		/// </summary>
		/// <param name="factory">The underlying connection factory</param>
		public DatabaseConnectionFactory(IDbConnectionFactory factory)
		{
			_factory = factory;
		}

		/// <inheritdoc />
		public IDbConnection Create() => _factory.CreateDbConnection();

		/// <inheritdoc />
		public Task<IDbConnection> Open() => _factory.OpenAsync();
	}
}