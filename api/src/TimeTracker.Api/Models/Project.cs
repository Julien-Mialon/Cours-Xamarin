using ServiceStack.DataAnnotations;
using Storm.Api.Core.Models;

namespace TimeTracker.Api.Models;

[Alias("Projects")]
public class Project : BaseEntityWithAutoIncrement
{
    [References(typeof(User))]
    public long UserId { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    public string Name { get; set; }

    [StringLength(StringLengthAttribute.MaxText)]
    public string Description { get; set; }
}