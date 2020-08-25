using MCA.GameSense.Models;
using System;
using System.Collections.Generic;

namespace MCA.GameSense.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string gameId = "CLOCK";
            string eventTimeId = "TIME";

            var coreProp = CorePropLoader.Load();
            Console.WriteLine(coreProp);

            try
            {
                using (GameSenseClient gameSense = new GameSenseClient("http://" + coreProp.Address))
                {
                    var result = gameSense.RegisterGame(gameId, "Clock", "Michaël Carpentier");

                    result = gameSense.BindEvent(gameId, eventTimeId, handlers: new [] { 
                        new Handler()
                        {
                            Data = new []
                            {
                                new ScreenFrameData()
                                {
                                    Prefix = "It's ",
                                    Icon = EventIcon.Clock
                                }
                            }
                        }
                    });

                    result = gameSense.SendEvent(gameId, eventTimeId, DateTime.Now.ToLongTimeString());

                    // Clear
                    result = gameSense.RemoveEvent(gameId, eventTimeId);
                    result = gameSense.RemoveGame(gameId);
                }
            }
            catch(AggregateException ae)
            {
                Console.WriteLine(ae.Message);
            }
        }
    }
}
