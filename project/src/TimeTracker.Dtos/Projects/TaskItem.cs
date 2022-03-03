using System.Collections.Generic;
using Newtonsoft.Json;

namespace TimeTracker.Dtos.Projects
{
    public class TaskItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("times")]
        public List<TimeItem> Times { get; set; }
    }
}