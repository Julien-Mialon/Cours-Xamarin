using System.Collections.Generic;
using Common.SqlMigrations.Models;

namespace TD.SqlMigrations
{
	internal class TdMigrations : BaseMigrationModule
	{
		public TdMigrations() : base("TD")
		{
		}

		public override List<IMigrationOperation> Operations { get; } = new List<IMigrationOperation>
		{
            new Migration001(),
		};
	}
}