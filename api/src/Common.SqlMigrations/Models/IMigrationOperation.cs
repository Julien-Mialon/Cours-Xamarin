using System.Data;
using System.Threading.Tasks;

namespace Common.SqlMigrations.Models
{
	public interface IMigrationOperation
	{
		int Number { get; }

		Task Apply(IDbConnection db);
		
		Task Revert(IDbConnection db);
	}
}