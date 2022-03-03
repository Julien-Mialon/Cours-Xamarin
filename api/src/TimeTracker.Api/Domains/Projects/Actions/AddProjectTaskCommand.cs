using Storm.Api.Core.Extensions;
using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class AddProjectTaskCommandParameter
{
    public long ProjectId { get; set; }
    public AddTaskRequest Request { get; set; }
}

public class AddProjectTaskCommand : AuthenticatedAction<AddProjectTaskCommandParameter, TaskItem>
{
    public AddProjectTaskCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(AddProjectTaskCommandParameter parameter)
    {
        return base.ValidateParameter(parameter)
               && parameter.Request is not null
               && parameter.Request.Name.IsNotNullOrEmpty();
    }

    protected override async Task<TaskItem> Action(AddProjectTaskCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();
        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();

        IProjectTaskService projectTaskService = Resolve<IProjectTaskService>();

        ProjectTask projectTask = await projectTaskService.Add(project, new ProjectTask
        {
            Name = parameter.Request.Name,
        });

        return projectTask.ToDtos();
    }
}