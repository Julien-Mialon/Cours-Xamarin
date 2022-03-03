namespace TimeTracker.Api.Domains.Projects.Converters;

public static class ValidationExtensions
{
    private static readonly DateTime MinDate = new DateTime(1900, 1, 1);
    private static readonly DateTime MaxDate = new DateTime(2200, 12, 31);
    
    public static bool IsValidDate(this DateTime date)
    {
        return MinDate < date && date < MaxDate;
    }
}