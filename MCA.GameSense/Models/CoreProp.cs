using Newtonsoft.Json;

namespace MCA.GameSense.Models
{
    public class CoreProp
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("encrypted_address")]
        public string EncryptedAddress { get; set; }

        public override string ToString()
        {
            return string.Format("Address: {0} - EncryptedAddress: {1}", this.Address, this.EncryptedAddress);
        }
    }
}
