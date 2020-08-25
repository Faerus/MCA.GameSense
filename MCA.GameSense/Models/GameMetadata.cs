using Newtonsoft.Json;

namespace MCA.GameSense.Models
{
    public class GameMetadata : BaseGameModel
    {
        /// <summary>
        /// User-friendly name displayed in SSE. If this is not set, your game will show up as the game string sent with your data.
        /// </summary>
        [JsonProperty("game_display_name", NullValueHandling = NullValueHandling.Ignore)]
        public string GameDisplayName { get; set; }

        /// <summary>
        /// Developer name displayed underneath the game name in SSE. This line is omitted in SSE if the metadata field is not set.
        /// </summary>
        [JsonProperty("developer", NullValueHandling = NullValueHandling.Ignore)]
        public string Developer { get; set; }

        /// <summary>
        /// By default, SSE will return to default behavior when the stop_game call is made or when no events have been received for 15 seconds. This can be used to customize that length of time between 1 and 60 seconds.
        /// Min: 1000
        /// Max: 60000
        /// </summary>
        [JsonProperty("deinitialize_timer_length_ms", NullValueHandling = NullValueHandling.Ignore)]
        public int? DeinitializeTimeLengthMs { get; set; }
    }
}
