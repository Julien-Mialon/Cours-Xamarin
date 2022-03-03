using ServiceStack.DataAnnotations;
using Storm.Api.Core.Models;

namespace TimeTracker.Api.Models;

[Alias("Users")]
public class User : BaseEntityWithAutoIncrement
{
    [StringLength(256)]
    [Index]
    public string Email { get; set; }

    [StringLength(256)]
    public string FirstName { get; set; }

    [StringLength(256)]
    public string LastName { get; set; }
}