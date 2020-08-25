using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCA.GameSense.Models
{
    public class EventData
    {
        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("frame", NullValueHandling = NullValueHandling.Ignore)]
        public object Frame { get; set; }
    }
}
