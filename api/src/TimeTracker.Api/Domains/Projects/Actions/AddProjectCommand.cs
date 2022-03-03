using Storm.Api.Core.Validations;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class AddProjectCommandParameter
{
    public AddProjectRequest Request { get; set; }
}

public class AddProjectCommand : AuthenticatedAction<AddProjectCommandParameter, ProjectItem>
{
    public AddProjectCommand(IServiceProvider services) : base(services)
    {
    }

    protected override bool ValidateParameter(AddProjectCommandParameter parameter)
    {
        return base.ValidateParameter(parameter)
               && parameter.Request is not null
               && parameter.Request.Name.IsNotNullOrEmpty();
    }

    protected override async Task<ProjectItem> Action(AddProjectCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();

        Project project = await projectService.Add(Account, new Project()
        {
            Name = parameter.Request.Name,
            Description = parameter.Request.Description,
        });

        return project.ToDtos(new Dictionary<long, long>
        {
            [project.Id] = 0
        });
    }
}