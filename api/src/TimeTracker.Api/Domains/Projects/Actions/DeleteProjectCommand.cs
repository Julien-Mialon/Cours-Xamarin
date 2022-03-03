using Storm.Api.Core;
using Storm.Api.Core.Extensions;
using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class DeleteProjectCommandParameter
{
    public long ProjectId { get; set; }
}

public class DeleteProjectCommand : AuthenticatedAction<DeleteProjectCommandParameter, Unit>
{
    public DeleteProjectCommand(IServiceProvider services) : base(services)
    {
    }

    protected override async Task<Unit> Action(DeleteProjectCommandParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();

        Project project = await projectService.Project(Account, parameter.ProjectId);
        project.NotFoundIfNull();

        project.IsDeleted = true;
        await projectService.Update(project);

        return Unit.Default;
    }
}