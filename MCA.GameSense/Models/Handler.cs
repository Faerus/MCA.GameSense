using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MCA.GameSense.Models
{
    public enum DeviceType
    {
        /*keyboard,
        mouse,
        headset,
        indicator,
        rgb-zoned-device,
        rgb-per-key-zones,
        tactile,*/
        [EnumMember(Value = "screened")]
        Screened,
        /*screened-128x36,
        screened-128x40,
        screened-128x48,
        screened-128x52*/
    }

    public enum Zone
    {
        /*[EnumMember("function-keys")]
        FunctionKeys,*/
        [EnumMember(Value = "one")]
        One
    }

    public enum Mode
    {
        [EnumMember(Value = "screen")]
        Screen
    }

    public class Handler
    {
        /// <summary>
        /// The device type to which you wish to apply the effect
        /// </summary>
        [JsonProperty("device-type"), JsonConverter(typeof(StringEnumConverter))]
        public DeviceType DeviceType { get; set; }

        /// <summary>
        /// The zone effected
        /// </summary>
        [JsonProperty("zone"), JsonConverter(typeof(StringEnumConverter))]
        public Zone Zone { get; set; }

        [JsonProperty("mode"), JsonConverter(typeof(StringEnumConverter))]
        public Mode Mode { get; set; }

        // TODO: create handler data class hierarchy
        [JsonProperty("datas", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<ScreenFrameData> Data { get; set; }
    }
}
