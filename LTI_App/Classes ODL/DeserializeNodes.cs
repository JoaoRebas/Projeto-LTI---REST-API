using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public partial class DeserializeNodes
    {
        [JsonProperty("nodes")]
        public NodeList NodesList { get; set; }
    }

    public partial class DeserializeNodes
    {
        public static DeserializeNodes FromJson(string json) => JsonConvert.DeserializeObject<DeserializeNodes>(json, ConverterNodes.Settings);
    }
}
