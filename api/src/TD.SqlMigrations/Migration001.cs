using System.Data;
using System.Threading.Tasks;
using Common.SqlMigrations.Models;
using ServiceStack.OrmLite;
using TD.Api.Models;

namespace TD.SqlMigrations
{
	internal class Migration001 : BaseMigration
	{
		public Migration001() : base(1) { }

		public override Task Apply(IDbConnection db)
		{	
			db.CreateTable<ImageModel>();
			db.CreateTable<Place>();
			db.CreateTable<Comment>();
			
			return Task.CompletedTask;
		}

		public override Task Revert(IDbConnection db)
		{
			db.DropTable<Comment>();
			db.DropTable<Place>();
			db.DropTable<ImageModel>();

			return Task.CompletedTask;
		}
	}
}