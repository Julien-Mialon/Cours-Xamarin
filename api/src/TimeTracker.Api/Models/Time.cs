using ServiceStack.DataAnnotations;
using Storm.Api.Core.Models;

namespace TimeTracker.Api.Models;

[Alias("Times")]
public class Time : BaseEntityWithAutoIncrement
{
    [References(typeof(ProjectTask))]
    public long ProjectTaskId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}