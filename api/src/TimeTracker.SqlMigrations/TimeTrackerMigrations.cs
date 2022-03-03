using Storm.SqlMigrations;

namespace TimeTracker.SqlMigrations;

public class TimeTrackerMigrations : BaseMigrationModule
{
    public override List<IMigration> Operations { get; } = new List<IMigration>
    {
        new Migration01(),
        new Migration02(),
    };

    public TimeTrackerMigrations() : base(nameof(TimeTrackerMigrations))
    {
    }
}