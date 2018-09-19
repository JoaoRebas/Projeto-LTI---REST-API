using Newtonsoft.Json;

namespace LTI_App.Instances_OPS
{
    public partial class DeserializeServer
    {
        [JsonProperty("server")]
        public ServerIndividual Server { get; set; }
    }

    public partial class DeserializeServer
    {
        public static DeserializeServer FromJson(string json) => JsonConvert.DeserializeObject<DeserializeServer>(json, ConverterServer.Settings);
    }
}
