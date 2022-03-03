using Storm.Api.Core.Extensions;
using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class UpdateProjectCommandParameter
{
    public long ProjectId { get; set; }
    
    public AddProjectRequest Request { get; set; }
}

public class UpdateProjectCommand : AuthenticatedAction<UpdateProjectCommandParameter, ProjectItem>
{
    public UpdateProjectCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(UpdateProjectCommandParameter parameter)
    {
        return base.ValidateParameter(parameter)
               && parameter.Request is not null
               && parameter.Request.Name.IsNotNullOrEmpty();
    }
    
    protected override async Task<ProjectItem> Action(UpdateProjectCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();

        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();

        project.Name = parameter.Request.Name;
        project.Description = parameter.Request.Description;
        await projectService.Update(project);

        var times = await projectService.TimeOfProjects(Account);

        return project.ToDtos(times);
    }
}