using Storm.Api.Core.Extensions;
using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class AddTimeCommandParameter
{
    public long ProjectId { get; set; }
    public long ProjectTaskId { get; set; }
    public AddTimeRequest Request { get; set; }
}

public class AddTimeCommand : AuthenticatedAction<AddTimeCommandParameter, TimeItem>
{
    public AddTimeCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(AddTimeCommandParameter parameter)
    {
        return base.ValidateParameter(parameter)
               && parameter.Request is not null
               && parameter.Request.StartTime.IsValidDate()
               && parameter.Request.EndTime.IsValidDate()
               && parameter.Request.StartTime < parameter.Request.EndTime;
    }

    protected override async Task<TimeItem> Action(AddTimeCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();
        
        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();
        ProjectTask projectTask = await projectTaskService.TaskOf(project, parameter.ProjectTaskId);
        projectTask.NotFoundIfNull();
        
        ITimeService timeService = Resolve<ITimeService>();
        var time = await timeService.Add(projectTask, new Time()
        {
            StartTime = parameter.Request.StartTime,
            EndTime = parameter.Request.EndTime,
        });

        return time.ToDtos();
    }
}