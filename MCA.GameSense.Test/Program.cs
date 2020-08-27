using MCA.GameSense.Models;
using System;
using System.Net.Http;
using System.Threading;

namespace MCA.GameSense.Clock
{
    class Program
    {
        static void Main(string[] args)
        {
            var coreProp = CorePropLoader.Load();
            using (GameSenseClient gameSense = new GameSenseClient(coreProp.Address))
            {
                try
                {
                    string gameId = "CLOCK";
                    var result = gameSense.RegisterGame(gameId, "Clock", "Michaël Carpentier");

                    string eventTimeId = "TIME";
                    gameSense.BindEvent(gameId, eventTimeId, handlers: new[] {
                        new Handler() {
                            Data = new [] {
                                new ScreenFrameData() { IconId = EventIcon.Clock }
                            }
                        }
                    });

                    using (Timer timer = new Timer(o => gameSense.SendEvent(gameId, eventTimeId, DateTime.Now.ToLongTimeString()), null, 0, 1000))
                    {
                        Thread.Sleep(Timeout.Infinite); // TODO: find a better way !
                    }
                }
                catch (HttpRequestException re)
                {
                    Console.WriteLine(re.Message);
                }
            }
        }
    }
}
