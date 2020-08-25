using MCA.GameSense.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MCA.GameSense
{
    public class GameSenseClient : IDisposable
    {
        private HttpClient Client { get; set; }

        public GameSenseClient(string baseUrl)
        {
            this.Client = new HttpClient();
            this.Client.BaseAddress = new Uri(baseUrl);
            this.Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpResponseMessage RegisterGame(string game, string gameDisplayName = null, string developer = null, int? deinitializeTimeLengthMs = null)
        {
            GameMetadata content = new GameMetadata() {
                Game = game,
                GameDisplayName = gameDisplayName,
                Developer = developer,
                DeinitializeTimeLengthMs = deinitializeTimeLengthMs
            };

            return this.Client.PostAsync("game_metadata", this.SerializeToJsonHttpContent(content)).Result;
        }

        private StringContent SerializeToJsonHttpContent<T>(T item)
        {
            return new StringContent(
                JsonConvert.SerializeObject(item), 
                Encoding.UTF8, 
                "application/json"
            );
        }

        public void Dispose()
        {
            this.Client.Dispose();
        }
    }

}
