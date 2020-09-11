using MCA.GameSense.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MCA.GameSense
{
    public class GameSenseClient : IDisposable
    {
        private HttpClient Client { get; set; }

        public string Game { get; set; }

        public GameSenseClient(string baseUrl, string game)
        {
            this.Game = game;

            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri(baseUrl);
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// GameSense™ is initialized on devices when the first event for a game is received. It is deactivated when 
        /// no events have been received within its timeout period, which defaults to 15 seconds. This means that your
        /// game should send at least one event every 15 seconds if you want the game state to continue to be fully 
        /// represented on the user's devices.
        /// </summary>
        public HttpResponseMessage SendHeartbeat()
        {
            BaseGameModel payload = new BaseGameModel()
            {
                Game = this.Game
            };

            return this.Client.PostAsync("game_heartbeat", this.SerializeToJsonHttpContent(payload)).Result;
        }

        /// <summary>
        /// Your game is automatically registered with SteelSeries Engine the system when you register or 
        /// bind any events. However, you can use another call to set various pieces of metadata.
        /// </summary>
        public HttpResponseMessage RegisterGame(string gameDisplayName = null, string developer = null, int? deinitializeTimeLengthMs = null)
        {
            GameMetadata payload = new GameMetadata()
            {
                Game = this.Game,
                GameDisplayName = gameDisplayName,
                Developer = developer,
                DeinitializeTimeLengthMs = deinitializeTimeLengthMs
            };

            return this.Client.PostAsync("game_metadata", this.SerializeToJsonHttpContent(payload)).Result;
        }

        public HttpResponseMessage RemoveGame()
        {
            BaseGameModel payload = new BaseGameModel()
            {
                Game = this.Game,
            };

            return this.Client.PostAsync("remove_game", this.SerializeToJsonHttpContent(payload)).Result;
        }

        /// <summary>
        /// Note: It is not necessary to both bind and register an event. The difference is that event registration 
        /// does not specify default (pre user customization) behavior for an event, whereas event binding does.
        /// </summary>
        public HttpResponseMessage RegisterEvent(string eventName, EventIcon? iconId = null, int? minValue = null, int? maxValue = null, bool? valueOptional = null)
        {
            EventMetadata payload = new EventMetadata()
            {
                Game = this.Game,
                Event = eventName,
                IconId = iconId,
                MinValue = minValue,
                MaxValue = maxValue,
                ValueOptional = valueOptional
            };

            return this.Client.PostAsync("register_game_event", this.SerializeToJsonHttpContent(payload)).Result;
        }

        public HttpResponseMessage BindEvent(string eventName, EventIcon? iconId = null, int? minValue = null, int? maxValue = null, bool? valueOptional = null, IEnumerable<Handler> handlers = null)
        {
            EventMetadata payload = new EventMetadata()
            {
                Game = this.Game,
                Event = eventName,
                IconId = iconId,
                MinValue = minValue,
                MaxValue = maxValue,
                ValueOptional = valueOptional,
                Handlers = handlers
            };

            return this.BindEvent(JsonConvert.SerializeObject(payload));
        }

        public HttpResponseMessage BindEvent(string jSonPayload)
        {
            return this.Client.PostAsync("bind_game_event", this.JsontoStringContent(jSonPayload)).Result;
        }

        public HttpResponseMessage RemoveEvent(string eventName)
        {
            BaseEventModel payload = new BaseEventModel()
            {
                Game = this.Game,
                Event = eventName
            };

            return this.Client.PostAsync("remove_game_event", this.SerializeToJsonHttpContent(payload)).Result;
        }

        public HttpResponseMessage SendEvent(string eventName, object eventValue, object frame = null)
        {
            Event payload = new Event()
            {
                Game = this.Game,
                Event = eventName,
                Data = new EventData()
                {
                    Value = eventValue,
                    Frame = frame
                }
            };

            return this.SendEvent(JsonConvert.SerializeObject(payload));
        }

        public HttpResponseMessage SendEvent(string jSonPayload)
        {
            return this.Client.PostAsync("game_event", this.JsontoStringContent(jSonPayload)).Result;
        }

        private StringContent SerializeToJsonHttpContent<T>(T item)
        {
            return JsontoStringContent(JsonConvert.SerializeObject(item));
        }

        private StringContent JsontoStringContent(string json)
        {
            Debug.WriteLine(json);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }
    }

}
