using Storm.Api.Core.Extensions;
using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class UpdateTimeCommandParameter
{
    public long ProjectId { get; set; }
    public long ProjectTaskId { get; set; }
    public long TimeId { get; set; }
    public AddTimeRequest Request { get; set; }
}

public class UpdateTimeCommand : AuthenticatedAction<UpdateTimeCommandParameter, TimeItem>
{
    public UpdateTimeCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(UpdateTimeCommandParameter parameter)
    {
        return base.ValidateParameter(parameter)
               && parameter.Request is not null
               && parameter.Request.StartTime.IsValidDate()
               && parameter.Request.EndTime.IsValidDate()
               && parameter.Request.StartTime < parameter.Request.EndTime;
    }
    
    protected override async Task<TimeItem> Action(UpdateTimeCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();
        
        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();
        ProjectTask projectTask = await projectTaskService.TaskOf(project, parameter.ProjectTaskId);
        projectTask.NotFoundIfNull();
        
        ITimeService timeService = Resolve<ITimeService>();
        Time time = await timeService.TimeOf(projectTask, parameter.TimeId);
        time.StartTime = parameter.Request.StartTime;
        time.EndTime = parameter.Request.EndTime;
        await timeService.Update(time);
        
        return time.ToDtos();
    }
}