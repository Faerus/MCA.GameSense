using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCA.GameSense.Models
{
    public class Event : BaseEventModel
    {
        [JsonProperty("data")]
        public EventData Data { get; set; }
    }
}
