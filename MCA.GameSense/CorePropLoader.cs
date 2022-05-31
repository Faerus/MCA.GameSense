using MCA.GameSense.Models;
using Newtonsoft.Json;
using System;
using System.IO;

namespace MCA.GameSense
{
    public static class CorePropLoader
    {
        private const string ADDRESS_PREFIX = "http://";
        private const string ENCRYPTED_ADDRESS_PREFIX = "https://";

        private static string CorePropPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
            @"SteelSeries\SteelSeries Engine 3\coreProps.json"
        );

        public static CoreProp Load()
        {
            CoreProp coreProp = JsonConvert.DeserializeObject<CoreProp>(File.ReadAllText(CorePropPath));

            if(coreProp.Address != null && !coreProp.Address.StartsWith(ADDRESS_PREFIX))
            {
                coreProp.Address = string.Concat(ADDRESS_PREFIX, coreProp.Address);
            }

            if (coreProp.EncryptedAddress != null && !coreProp.EncryptedAddress.StartsWith(ENCRYPTED_ADDRESS_PREFIX))
            {
                coreProp.EncryptedAddress = string.Concat(ENCRYPTED_ADDRESS_PREFIX, coreProp.EncryptedAddress);
            }

            return coreProp;
        }
    }
}
