using Storm.Api.Core.Extensions;
using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class UpdateProjectTaskCommandParameter
{
    public long ProjectId { get; set; }
    public long ProjectTaskId { get; set; }
    public AddTaskRequest Request { get; set; }
}

public class UpdateProjectTaskCommand : AuthenticatedAction<UpdateProjectTaskCommandParameter, TaskItem>
{
    public UpdateProjectTaskCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(UpdateProjectTaskCommandParameter parameter)
    {
        return base.ValidateParameter(parameter)
               && parameter.Request is not null
               && parameter.Request.Name.IsNotNullOrEmpty();
    }
    
    protected override async Task<TaskItem> Action(UpdateProjectTaskCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();
        
        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();
        ProjectTask projectTask = await projectTaskService.TaskOfWithTime(project, parameter.ProjectTaskId);
        projectTask.NotFoundIfNull();

        projectTask.Name = parameter.Request.Name;
        await projectTaskService.Update(projectTask);

        return projectTask.ToDtos();
    }
}