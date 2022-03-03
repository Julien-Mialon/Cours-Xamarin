using System;
using Newtonsoft.Json;

namespace TimeTracker.Dtos.Projects
{
    public class AddTimeRequest
    {
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }
        
        [JsonProperty("end_time")]
        public DateTime EndTime { get; set; }
    }
}