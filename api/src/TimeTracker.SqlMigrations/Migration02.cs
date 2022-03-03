using System.Data;
using ServiceStack.OrmLite;
using Storm.SqlMigrations;
using TimeTracker.Api.Models;

namespace TimeTracker.SqlMigrations;

public class Migration02 : BaseMigration
{
    public Migration02() : base(2)
    {
    }

    public override async Task Apply(IDbConnection db)
    {
        await db.InsertAsync(new ApiClient()
        {
            ClientId = "MOBILE",
            ClientSecret = "COURS",
        });
    }
}