using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class ListProjectTaskQueryParameter
{
    public long ProjectId { get; set; }
}

public class ListProjectTaskQuery : AuthenticatedAction<ListProjectTaskQueryParameter, List<TaskItem>>
{
    public ListProjectTaskQuery(IServiceProvider services) : base(services)
    {
    }

    protected override async Task<List<TaskItem>> Action(ListProjectTaskQueryParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();
        
        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();
        List<ProjectTask> projectTasks = await projectTaskService.TasksOf(project);

        return projectTasks.ToDtos();
    }
}