using System;

namespace MCA.GameSense.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var coreProp = CorePropLoader.Load();
            Console.WriteLine(coreProp);

            using (GameSenseClient gameSense = new GameSenseClient("http://" + coreProp.Address))
            {
                var result = gameSense.RegisterGame("CLOCK", "Clock", "Michaël Carpentier");
            }
        }
    }
}
