using Storm.Api.Core;
using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class DeleteTimeCommandParameter
{
    public long ProjectId { get; set; }
    public long ProjectTaskId { get; set; }
    public long TimeId { get; set; }
}

public class DeleteTimeCommand : AuthenticatedAction<DeleteTimeCommandParameter, Unit>
{
    public DeleteTimeCommand(IServiceProvider services) : base(services)
    {
    }

    protected override async Task<Unit> Action(DeleteTimeCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();
        
        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();
        ProjectTask projectTask = await projectTaskService.TaskOf(project, parameter.ProjectTaskId);
        projectTask.NotFoundIfNull();
        
        ITimeService timeService = Resolve<ITimeService>();
        Time time = await timeService.TimeOf(projectTask, parameter.TimeId);
        time.IsDeleted = true;
        await timeService.Update(time);

        return Unit.Default;
    }
}