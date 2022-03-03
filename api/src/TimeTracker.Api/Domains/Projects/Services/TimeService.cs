using ServiceStack.OrmLite;
using Storm.Api.Core.Extensions;
using Storm.Api.Core.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Projects.Services;

public interface ITimeService
{
    Task<Time> TimeOf(ProjectTask task, long timeId);
    Task<Time> Add(ProjectTask task, Time time);
    Task Update(Time time);
}

public class TimeService : BaseDatabaseService, ITimeService
{
    public TimeService(IServiceProvider services) : base(services)
    {
    }

    public async Task<Time> TimeOf(ProjectTask task, long timeId)
    {
        return await UseConnection(async connection =>
        {
            return await connection.From<Time>()
                .NotDeleted()
                .Where(x => x.Id == timeId && x.ProjectTaskId == task.Id)
                .AsSingleAsync(connection);
        });
    }

    public async Task<Time> Add(ProjectTask task, Time time)
    {
        return await UseConnection(async connection =>
        {
            time.ProjectTaskId = task.Id;
            time.Id = await connection.InsertAsync(time, selectIdentity: true);
            return time;
        });
    }
    
    public async Task Update(Time time)
    {
        await UseConnection(async connection =>
        {
            await connection.UpdateAsync(time);
        });
    }
}