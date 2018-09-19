using Newtonsoft.Json;

namespace LTI_App.Instances_OPS
{
    public class Private
    {
        [JsonProperty("OS-EXT-IPS-MAC:mac_addr")]
        public string OsExtIpsMacMacAddr { get; set; }

        [JsonProperty("version")]
        public long Version { get; set; }

        [JsonProperty("addr")]
        public string Addr { get; set; }

        [JsonProperty("OS-EXT-IPS:type")]
        public string OsExtIpsType { get; set; }
    }
}