using Newtonsoft.Json;

namespace MCA.GameSense.Models
{
    public class BaseEventModel : BaseGameModel
    {
        /// <summary>
        /// Event identifier, mandatory
        /// Limited to uppercase A-Z, 0-9, hyphen, and underscore characters.
        /// </summary>
        [JsonProperty("event")]
        public string Event { get; set; }
    }
}
