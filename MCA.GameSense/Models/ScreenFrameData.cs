using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCA.GameSense.Models
{
    public abstract class HandlerData
    {
    }

    public abstract class ScreenFrameData : HandlerData
    {
        #region frame-modifiers-data

        [JsonProperty("length-millis", NullValueHandling = NullValueHandling.Ignore)]
        public int? LengthMs { get; set; }

        [JsonProperty("icon-id", NullValueHandling = NullValueHandling.Ignore)]
        public EventIcon? IconId { get; set; }

        [JsonProperty("repeats", NullValueHandling = NullValueHandling.Ignore)]
        public bool? Repeats { get; set; }

        #endregion frame-modifiers-data
    }

    public interface ILineData
    {
        #region has-progress-bar

        [JsonProperty("has-progress-bar", NullValueHandling = NullValueHandling.Ignore)]
        bool? HasProgressBar { get; set; }

        #endregion has-progress-bar

        #region text-modifiers-data

        [JsonProperty("has-text", NullValueHandling = NullValueHandling.Ignore)]
        bool HasText { get; set; }

        [JsonProperty("prefix", NullValueHandling = NullValueHandling.Ignore)]
        string Prefix { get; set; }

        [JsonProperty("suffix", NullValueHandling = NullValueHandling.Ignore)]
        string Suffix { get; set; }

        [JsonProperty("bold", NullValueHandling = NullValueHandling.Ignore)]
        bool? Bold { get; set; }

        [JsonProperty("wrap", NullValueHandling = NullValueHandling.Ignore)]
        int? Wrap { get; set; }

        #endregion text-modifiers-data

        #region data_accessor_data

        [JsonProperty("arg", NullValueHandling = NullValueHandling.Ignore)]
        string Arg { get; set; }

        [JsonProperty("context-frame-key", NullValueHandling = NullValueHandling.Ignore)]
        string ContextFrameKey { get; set; }

        #endregion data_accessor_data
    }

    public class LineData : ILineData
    {
        #region ILineData

        public bool? HasProgressBar { get; set; }
        public bool HasText { get; set; } = true;
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool? Bold { get; set; }
        public int? Wrap { get; set; }
        public string Arg { get; set; }
        public string ContextFrameKey { get; set; }

        #endregion ILineData
    }

    public class SingleLineFrameData : ScreenFrameData, ILineData
    {
        #region ILineData

        public bool? HasProgressBar { get; set; }
        public bool HasText { get; set; } = true;
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool? Bold { get; set; }
        public int? Wrap { get; set; }
        public string Arg { get; set; }
        public string ContextFrameKey { get; set; }

        #endregion ILineData
    }

    public class MultiLineFrameData : ScreenFrameData
    {
        [JsonProperty("lines", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<LineData> Lines { get; set; }
    }

    public class ImageFrameData : ScreenFrameData
    {
        [JsonProperty("image-data", NullValueHandling = NullValueHandling.Ignore)]
        public byte[] Image { get; set; }
    }
}