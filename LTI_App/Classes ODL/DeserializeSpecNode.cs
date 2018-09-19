using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.Classes_ODL
{

    public partial class DeserializeSpecNode
    {
        [JsonProperty("node")]
        public List<Node> NodeList { get; set; }
    }

    public partial class DeserializeSpecNode
    {
        public static DeserializeSpecNode FromJson(string json) => JsonConvert.DeserializeObject<DeserializeSpecNode>(json, ConverterSpecNode.Settings);
    }
}
