using Newtonsoft.Json;

namespace MCA.GameSense.Models
{
    public abstract class HandlerData
    {
    }

    public class ScreenFrameData : HandlerData
    {
        [JsonProperty("has-text", NullValueHandling = NullValueHandling.Ignore)]
        public bool HasText { get; set; }

        [JsonProperty("has-progress-bar", NullValueHandling = NullValueHandling.Ignore)]
        public bool? HasProgressBar { get; set; }


        [JsonProperty("bold", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Bold { get; set; }

        [JsonProperty("icon-id", NullValueHandling = NullValueHandling.Ignore)]
        public EventIcon? IconId { get; set; }

        [JsonProperty("arg", NullValueHandling = NullValueHandling.Ignore)]
        public string Arg { get; set; }

        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string Prefix { get; set; }

        [JsonProperty("suffix", NullValueHandling = NullValueHandling.Ignore)]
        public string Suffix { get; set; }

        [JsonProperty("length-millis", NullValueHandling = NullValueHandling.Ignore)]
        public int? LengthMs { get; set; }

        [JsonProperty("repeats", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Repeats { get; set; }

        [JsonProperty("context-frame-key", NullValueHandling = NullValueHandling.Ignore)]
        public string ContextFrameKey { get; set; }

        [JsonProperty("wrap", NullValueHandling = NullValueHandling.Ignore)]
        public int? Wrap { get; set; }

        public ScreenFrameData()
        {
            this.HasText = true;
        }
    }
}
