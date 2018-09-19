using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public partial class NodeList
    {
        [JsonProperty("node")]
        public List<Node> Nodes { get; set; }
    }
}
