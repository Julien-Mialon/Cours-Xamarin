using Newtonsoft.Json;

namespace TimeTracker.Dtos.Projects
{
    public class AddProjectRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
    }
}