using Newtonsoft.Json;

namespace TimeTracker.Dtos.Projects
{
    public class ProjectItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("total_seconds")]
        public long TotalSeconds { get; set; }
    }
}