using System.Data;
using System.Threading.Tasks;

namespace Common.SqlMigrations.Models
{
	public abstract class BaseMigration : IMigrationOperation
	{
		public int Number { get; }
		
		protected BaseMigration(int number)
		{
			Number = number;
		}

		public abstract Task Apply(IDbConnection db);

		public abstract Task Revert(IDbConnection db);
	}
}