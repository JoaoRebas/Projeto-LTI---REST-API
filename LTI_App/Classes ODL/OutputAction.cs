using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class OutputAction
    {
        [JsonProperty("output-node-connector")]
        public string OutputNodeConnector { get; set; }

        [JsonProperty("max-length")]
        public long MaxLength { get; set; }
    }
}
