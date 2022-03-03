using Storm.Api.Core;
using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class DeleteProjectTaskCommandParameter
{
    public long ProjectId { get; set; }
    public long ProjectTaskId { get; set; }
}

public class DeleteProjectTaskCommand : AuthenticatedAction<DeleteProjectTaskCommandParameter, Unit>
{
    public DeleteProjectTaskCommand(IServiceProvider services) : base(services)
    {
    }

    protected override async Task<Unit> Action(DeleteProjectTaskCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();
        
        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();
        ProjectTask projectTask = await projectTaskService.TaskOf(project, parameter.ProjectTaskId);
        projectTask.NotFoundIfNull();

        projectTask.IsDeleted = true;
        await projectTaskService.Update(projectTask);

        return Unit.Default;
    }
}