using System;
using System.Data.Common;
using System.Diagnostics;
using ServiceStack.Logging;
using ServiceStack.MiniProfiler.Data;
using ServiceStack.OrmLite;
using ServiceStack.Text;

namespace Common.Core.Database
{
	internal class SqlProfiler : IDbProfiler
	{
		public void ExecuteStart(DbCommand profiledDbCommand, ExecuteType executeType)
		{
			Debug.WriteLine(profiledDbCommand.CommandText);
		}

		public void ExecuteFinish(DbCommand profiledDbCommand, ExecuteType executeType, DbDataReader reader)
		{
			
		}

		public void ReaderFinish(DbDataReader reader)
		{
			
		}

		public void OnError(DbCommand profiledDbCommand, ExecuteType executeType, Exception exception)
		{
			
		}

		public bool IsActive { get; } = true;
	}
	
	/// <summary>
	/// Builder to configure and create connection factory.
	/// </summary>
	public class DatabaseConfigurationBuilder
	{
		private const string AZURE_SQL_FORMAT = "Server=tcp:{0},1433;Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3};MultipleActiveResultSets=True;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
		private const string MYSQL_FORMAT = "Server={0};Port=3306;Database={1};UID={2};Password={3};SslMode=None";
		
		private string _connectionString;
		private IOrmLiteDialectProvider _dialectProvider;

		/// <summary>
		/// Create database factory from configuration
		/// </summary>
		/// <returns>A new connection factory</returns>
		public IDatabaseConnectionFactory CreateFactory()
		{
			LogManager.LogFactory = new ConsoleLogFactory(debugEnabled:true);
			
			string connectionString = _connectionString ?? throw new InvalidOperationException("Configuration has not been finished");
			IOrmLiteDialectProvider provider = _dialectProvider ?? throw new InvalidOperationException("Configuration has not been finished");
			OrmLiteConnectionFactory connectionFactory = new OrmLiteConnectionFactory(connectionString, provider)
			{
				ConnectionFilter = x => new ProfiledConnection(x, new SqlProfiler())
			};

			JsConfig.TreatEnumAsInteger = true;
			IDatabaseConnectionFactory factory = new DatabaseConnectionFactory(connectionFactory);
			OrmLiteConfig.DialectProvider.GetStringConverter().UseUnicode = true;
			return factory;
		}
		
		/// <summary>
		/// Configure to use SQLite with specific database file
		/// </summary>
		/// <param name="file">Path to database file</param>
		/// <returns>this</returns>
		public DatabaseConfigurationBuilder UseSqlite(string file)
		{
			_connectionString = file;
			_dialectProvider = SqliteDialect.Provider;
			return this;
		}

		/// <summary>
		/// Configure to use SQLite with in memory configuration (useful for testing)
		/// </summary>
		/// <returns>this</returns>
		public DatabaseConfigurationBuilder UseInMemorySqlite() => UseSqlite(":memory:");

		/// <summary>
		/// Configure to use SQL Server with specific connection string
		/// </summary>
		/// <param name="connectionString">Connection string to access SQL Server</param>
		/// <returns>this</returns>
		public DatabaseConfigurationBuilder UseMySql(string connectionString)
		{
			_connectionString = connectionString;
			_dialectProvider = MySqlDialect.Provider;
			return this;
		}
		
		/// <summary>
		/// Configure to use mysql
		/// </summary>
		/// <param name="host">Host of the mysql instance</param>
		/// <param name="database">Database to connect</param>
		/// <param name="login">Database user login</param>
		/// <param name="password">Database user password</param>
		/// <returns>this</returns>
		public DatabaseConfigurationBuilder UseMysql(string host, string database, string login, string password)
			=> UseMySql(string.Format(MYSQL_FORMAT, host, database, login, password));
	}
}