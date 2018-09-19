using Newtonsoft.Json;

namespace LTI_App
{
    public class AddressTrackerAddress
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mac")]
        public string Mac { get; set; }

        [JsonProperty("first-seen")]
        public long FirstSeen { get; set; }

        [JsonProperty("last-seen")]
        public long LastSeen { get; set; }

        [JsonProperty("ip")]
        public string Ip { get; set; }
    }
}