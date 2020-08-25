using Newtonsoft.Json;
using System.Collections.Generic;

namespace MCA.GameSense.Models
{
    /// <summary>
    /// <see cref="https://github.com/SteelSeries/gamesense-sdk/blob/master/doc/api/event-icons.md"/>
    /// </summary>
    public enum EventIcon
    {
        None,
        Health,
        Armor,
        Ammo,
        Money,
        Explosion,
        Kills,
        Headshot,
        Helmet,
        // Missing
        Hunger = 10,
        Air,
        Compass,
        Pickaxe,
        Potion,
        Clock,
        Lightning,
        Backpack,
        Symbol,
        Muted,
        Talking,
        Connect,
        Disconnect,
        Music,
        Play,
        Pause,
        // Missing
        CPU = 27,
        GPU,
        RAM,
        Assists,
        CreepScore,
        Dead,
        Dragon,
        // Missing
        Enemies = 35,
        GameStart,
        Gold,
        Health2,
        Kills2,
        Mana2,
        Teammates,
        Timer,
        Temperature
    }

    public class EventMetadata : BaseEventModel
    {
        [JsonProperty("min_value", NullValueHandling = NullValueHandling.Ignore)]
        public int? MinValue { get; set; }

        [JsonProperty("max_value", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxValue { get; set; }

        [JsonProperty("icon_id", NullValueHandling = NullValueHandling.Ignore)]
        public EventIcon? IconId { get; set; }

        /// <summary>
        /// If the value_optional key is set to true for an event, the handlers for the event will be processed each time 
        /// it is updated, even if a value key is not specified in the data or if the value key matches the previously 
        /// cached value. This is mainly useful for events that use context data rather than the event value to determine 
        /// what to display, such as some OLED screen events or for bitmap type lighting events
        /// </summary>
        [JsonProperty("value_optional", NullValueHandling = NullValueHandling.Ignore)]
        public bool? ValueOptional { get; set; }

        /// <summary>
        /// If the value_optional key is set to true for an event, the handlers for the event will be processed each time 
        /// it is updated, even if a value key is not specified in the data or if the value key matches the previously 
        /// cached value. This is mainly useful for events that use context data rather than the event value to determine 
        /// what to display, such as some OLED screen events or for bitmap type lighting events
        /// </summary>
        [JsonProperty("handlers", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<Handler> Handlers { get; set; }
    }
}
