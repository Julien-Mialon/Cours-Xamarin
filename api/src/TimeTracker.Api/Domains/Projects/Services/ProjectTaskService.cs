using ServiceStack.OrmLite;
using Storm.Api.Core.Extensions;
using Storm.Api.Core.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Projects.Services;

public interface IProjectTaskService
{
    Task<List<ProjectTask>> TasksOf(Project project);
    Task<ProjectTask> TaskOf(Project project, long taskId);
    Task<ProjectTask> TaskOfWithTime(Project project, long taskId);
    Task<ProjectTask> Add(Project project, ProjectTask task);
    Task Update(ProjectTask task);
}

public class ProjectTaskService : BaseDatabaseService, IProjectTaskService
{
    public ProjectTaskService(IServiceProvider services) : base(services)
    {
    }

    public async Task<List<ProjectTask>> TasksOf(Project project)
    {
        return await UseConnection(async connection =>
        {
            List<ProjectTask> results = new List<ProjectTask>();

            await connection.From<ProjectTask>()
                .LeftJoin<ProjectTask, Time>((task, time) => task.Id == time.ProjectTaskId)
                .NotDeleted()
                .Where<Time>(x => x.IsDeleted == null || x.IsDeleted == false)
                .Where(x => x.ProjectId == project.Id)
                .AsSelectMultiAsync(connection, connection.Mapper<ProjectTask, Time, ProjectTask>((task, time) =>
                {
                    if (results.Count == 0 || results[^1].Id != task.Id)
                    {
                        task.Times = new List<Time>();
                        results.Add(task);
                    }

                    if (time is not null)
                    {
                        results[^1].Times.Add(time);
                    }

                    return task;
                }));

            return results;
        });
    }

    public async Task<ProjectTask> TaskOf(Project project, long taskId)
    {
        return await UseConnection(async connection =>
        {
            return await connection.From<ProjectTask>()
                .NotDeleted()
                .Where(x => x.ProjectId == project.Id && x.Id == taskId)
                .AsSingleAsync(connection);
        });
    }

    public async Task<ProjectTask> TaskOfWithTime(Project project, long taskId)
    {
        return await UseConnection(async connection =>
        {
            ProjectTask result = null;

            await connection.From<ProjectTask>()
                .LeftJoin<ProjectTask, Time>((task, time) => task.Id == time.ProjectTaskId)
                .NotDeleted()
                .Where<Time>(x => x.IsDeleted == null || x.IsDeleted == false)
                .Where(x => x.ProjectId == project.Id && x.Id == taskId)
                .AsSelectMultiAsync(connection, connection.Mapper<ProjectTask, Time, ProjectTask>((task, time) =>
                {
                    if (result is null)
                    {
                        result = task;
                        result.Times = new();
                    }

                    if (time is not null)
                    {
                        result.Times.Add(time);
                    }

                    return task;
                }));

            return result;
        });
    }

    public async Task<ProjectTask> Add(Project project, ProjectTask task)
    {
        return await UseConnection(async connection =>
        {
            task.ProjectId = project.Id;
            task.Id = await connection.InsertAsync(task, selectIdentity: true);
            return task;
        });
    }

    public async Task Update(ProjectTask task)
    {
        await UseConnection(async connection => { await connection.UpdateAsync(task); });
    }
}