using System.Data;
using System.Threading.Tasks;

namespace Common.Core.Database
{
	/// <summary>
	/// Connection factory to use database
	/// </summary>
	public interface IDatabaseConnectionFactory
	{
		/// <summary>
		/// Create a database connection but do not open it yet
		/// </summary>
		/// <returns>The newly created connection</returns>
		IDbConnection Create();
		
		/// <summary>
		/// Create a database connection and open it
		/// </summary>
		/// <returns>The opened connection</returns>
		Task<IDbConnection> Open();
	}
}