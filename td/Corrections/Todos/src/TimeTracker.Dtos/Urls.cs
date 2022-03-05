namespace TimeTracker.Dtos
{
    public class Urls
    {
        public const string HOST = "https://timetracker.julienmialon.ovh";
        
        private const string ROOT = "api/v1";
        
        public const string USER_PROFILE = ROOT + "/me";
        public const string CREATE_USER = ROOT + "/register";
        public const string SET_USER_PROFILE = ROOT + "/me";
        
        public const string LOGIN = ROOT + "/login";
        public const string REFRESH_TOKEN = ROOT + "/refresh";

        public const string SET_PASSWORD = ROOT + "/password";

        public const string LIST_PROJECTS = ROOT + "/projects";
        public const string ADD_PROJECT = ROOT + "/projects";
        public const string UPDATE_PROJECT = ROOT + "/projects/{projectId}";
        public const string DELETE_PROJECT = ROOT + "/projects/{projectId}";
        
        public const string LIST_TASKS = ROOT + "/projects/{projectId}/tasks";
        public const string CREATE_TASK = ROOT + "/projects/{projectId}/tasks";
        public const string UPDATE_TASK = ROOT + "/projects/{projectId}/tasks/{taskId}";
        public const string DELETE_TASK = ROOT + "/projects/{projectId}/tasks/{taskId}";
        
        public const string ADD_TIME = ROOT + "/projects/{projectId}/tasks/{taskId}/times";
        public const string UPDATE_TIME = ROOT + "/projects/{projectId}/tasks/{taskId}/times/{timeId}";
        public const string DELETE_TIME = ROOT + "/projects/{projectId}/tasks/{taskId}/times/{timeId}";
    }
}