using ServiceStack.DataAnnotations;
using Storm.Api.Core.Models;

namespace TimeTracker.Api.Models;

[Alias("ProjectTasks")]
public class ProjectTask : BaseEntityWithAutoIncrement
{
    [References(typeof(Project))]
    public long ProjectId { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    public string Name { get; set; }
    
    [Ignore]
    public List<Time> Times { get; set; }
}