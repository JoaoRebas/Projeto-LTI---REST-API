using Newtonsoft.Json;

namespace LTI_App.Instances_OPS
{
    public class SecurityGroup
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}