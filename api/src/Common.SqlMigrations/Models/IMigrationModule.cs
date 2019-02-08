using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Common.SqlMigrations.Models
{
	public interface IMigrationModule
	{
		string Name { get; }
		
		List<IMigrationOperation> Operations { get; }

		Task StartMigrationOnModule(IDbConnection db);
		
		Task EndMigrationOnModule(IDbConnection db);
	}
}