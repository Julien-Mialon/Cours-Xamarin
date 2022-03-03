using TimeTracker.Api.Domains.Bases.Actions;
using TimeTracker.Api.Domains.Projects.Converters;
using TimeTracker.Api.Domains.Projects.Services;
using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Actions;

public class ListProjectQueryParameter
{
    
}

public class ListProjectQuery : AuthenticatedAction<ListProjectQueryParameter, List<ProjectItem>>
{
    public ListProjectQuery(IServiceProvider services) : base(services)
    {
    }

    protected override async Task<List<ProjectItem>> Action(ListProjectQueryParameter parameter)
    {
        IProjectService projectService = Resolve<IProjectService>();

        List<Project> projects = await projectService.Projects(Account);
        Dictionary<long, long> times = await projectService.TimeOfProjects(Account);

        return projects.ToDtos(times);
    }
}