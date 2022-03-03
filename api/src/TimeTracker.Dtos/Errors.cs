namespace TimeTracker.Dtos
{
    public class ErrorCodes
    {
        public const string EMAIL_ALREADY_EXISTS = nameof(EMAIL_ALREADY_EXISTS);
        public const string WEAK_PASSWORD = nameof(WEAK_PASSWORD);
        public const string NO_CREDENTIALS = nameof(NO_CREDENTIALS);
        public const string INVALID_PASSWORD = nameof(INVALID_PASSWORD);

        public const string PROJECT_NOT_EXISTS = nameof(PROJECT_NOT_EXISTS);
        public const string TASK_NOT_EXISTS = nameof(TASK_NOT_EXISTS);
        public const string TIME_NOT_EXISTS = nameof(TIME_NOT_EXISTS);
    }
}