using ServiceStack.OrmLite;
using Storm.Api.Core.Extensions;
using Storm.Api.Core.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Projects.Services;

public interface IProjectService
{
    Task<List<Project>> Projects(User user);
    Task<Project> Project(User user, long projectId);
    Task<Dictionary<long, long>> TimeOfProjects(User user);
    Task<Project> Add(User user, Project project);
    Task Update(Project project);
}

public class ProjectService : BaseDatabaseService, IProjectService
{
    public ProjectService(IServiceProvider services) : base(services)
    {
    }

    public async Task<List<Project>> Projects(User user)
    {
        return await UseConnection(async connection =>
        {
            return await connection.From<Project>()
                .NotDeleted()
                .Where(x => x.UserId == user.Id)
                .AsSelectAsync(connection);
        });
    }
    
    public async Task<Project> Project(User user, long projectId)
    {
        return await UseConnection(async connection =>
        {
            return await connection.From<Project>()
                .NotDeleted()
                .Where(x => x.UserId == user.Id && x.Id == projectId)
                .AsSingleAsync(connection);
        });
    }
    
    public async Task<Dictionary<long, long>> TimeOfProjects(User user)
    {
        return await UseConnection(async connection =>
        {
            Dictionary<long, long> result = new Dictionary<long, long>();

            await connection.From<Project>()
                .LeftJoin<Project, ProjectTask>((project, task) => project.Id == task.ProjectId)
                .LeftJoin<ProjectTask, Time>((task, item) => task.Id == item.ProjectTaskId)
                .NotDeleted()
                .Where<ProjectTask>(x => x.IsDeleted == false)
                .Where<Time>(x => x.IsDeleted == false)
                .Where(x => x.UserId == user.Id)
                .AsSelectMultiAsync(connection, connection.Mapper<Project, ProjectTask, Time, Project>((project, task, time) =>
                {
                    long seconds = 0;
                    if (time is not null)
                    {
                        seconds = Math.Max(0, (long)(time.EndTime - time.StartTime).TotalSeconds);
                    }
                    
                    if (result.ContainsKey(project.Id))
                    {
                        result[project.Id] += seconds;
                    }
                    else
                    {
                        result[project.Id] = seconds;
                    }
                    
                    return project;
                }));

            return result;
        });
    }

    public async Task<Project> Add(User user, Project project)
    {
        return await UseConnection(async connection =>
        {
            project.UserId = user.Id;
            project.Id = await connection.InsertAsync(project, selectIdentity: true);
            
            return project;
        });
    }

    public async Task Update(Project project)
    {
        await UseConnection(async connection =>
        {
            await connection.UpdateAsync(project);
        });
    }
}