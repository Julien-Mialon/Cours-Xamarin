using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Common.SqlMigrations.Models
{
	public abstract class BaseMigrationModule : IMigrationModule
	{
		protected BaseMigrationModule(string name)
		{
			Name = name;
		}

		public string Name { get; }
		
		public abstract List<IMigrationOperation> Operations { get; }
		
		public virtual Task StartMigrationOnModule(IDbConnection db)
		{
			return Task.CompletedTask;
		}

		public virtual Task EndMigrationOnModule(IDbConnection db)
		{
			return Task.CompletedTask;
		}
	}
}