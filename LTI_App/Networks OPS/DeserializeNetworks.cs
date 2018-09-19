using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.Networks_OPS
{
    public partial class DeserializeNetworks
    {
        
            [JsonProperty("networks")]
            public List<Network> Networks { get; set; }
        
    }
    public partial class DeserializeNetworks
        {
            public static DeserializeNetworks FromJson(string json) => JsonConvert.DeserializeObject<DeserializeNetworks>(json, ConverterNetworks.Settings);
        }
}
