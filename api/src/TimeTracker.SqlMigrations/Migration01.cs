using System.Data;
using ServiceStack.OrmLite;
using Storm.SqlMigrations;
using TimeTracker.Api.Models;

namespace TimeTracker.SqlMigrations;

public class Migration01 : BaseMigration
{
    public Migration01() : base(1)
    {
    }

    public override Task Apply(IDbConnection db)
    {
        db.CreateTable<User>();
        db.CreateTable<ApiClient>();
        db.CreateTable<LoginPasswordUserAuthentication>();
        db.CreateTable<AuthenticationToken>();
        db.CreateTable<Project>();
        db.CreateTable<ProjectTask>();
        db.CreateTable<Time>();

        return Task.CompletedTask;
    }
}