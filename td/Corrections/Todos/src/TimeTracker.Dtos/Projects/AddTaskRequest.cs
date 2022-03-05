using Newtonsoft.Json;

namespace TimeTracker.Dtos.Projects
{
    public class AddTaskRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}