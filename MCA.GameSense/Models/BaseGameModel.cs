using Newtonsoft.Json;

namespace MCA.GameSense.Models
{
    public class BaseGameModel
    {
        /// <summary>
        /// Game identifier, mandatory
        /// Limited to uppercase A-Z, 0-9, hyphen, and underscore characters.
        /// </summary>
        [JsonProperty("game")]
        public string Game { get; set; }
    }
}
