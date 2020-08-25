using MCA.GameSense.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MCA.GameSense
{
    public static class CorePropLoader
    {
        private static string CorePropPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            @"SteelSeries\SteelSeries Engine 3\coreProps.json"
        );

        public static CoreProp Load()
        {
            return JsonConvert.DeserializeObject<CoreProp>(File.ReadAllText(CorePropPath));
        }
    }
}
