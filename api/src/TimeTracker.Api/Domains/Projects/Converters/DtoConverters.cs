using TimeTracker.Api.Models;
using TimeTracker.Dtos.Projects;

namespace TimeTracker.Api.Domains.Projects.Converters;

public static class DtoConverters
{
    public static ProjectItem ToDtos(this Project project, Dictionary<long, long> times)
    {
        if (project is null)
        {
            return null;
        }

        times.TryGetValue(project.Id, out long time);

        return new()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            TotalSeconds = time
        };
    }

    public static List<ProjectItem> ToDtos(this List<Project> projects, Dictionary<long, long> times)
    {
        return projects?.ConvertAll(x => x.ToDtos(times)) ?? new List<ProjectItem>();
    }

    public static List<TaskItem> ToDtos(this List<ProjectTask> tasks)
    {
        return tasks?.ConvertAll(x => x.ToDtos()) ?? new List<TaskItem>();
    }
    
    public static TaskItem ToDtos(this ProjectTask task)
    {
        if (task is null)
        {
            return null;
        }
        
        return new()
        {
            Id = task.Id,
            Name = task.Name,
            Times = task.Times?.ConvertAll(x => x.ToDtos()) ?? new List<TimeItem>()
        };
    }

    public static TimeItem ToDtos(this Time time)
    {
        if (time is null)
        {
            return null;
        }

        return new()
        {
            Id = time.Id,
            StartTime = time.StartTime,
            EndTime = time.EndTime
        };
    }
}