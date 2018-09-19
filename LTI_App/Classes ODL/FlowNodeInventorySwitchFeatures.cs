using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class FlowNodeInventorySwitchFeatures
    {
        [JsonProperty("max_buffers")]
        public long MaxBuffers { get; set; }

        [JsonProperty("capabilities")]
        public List<string> Capabilities { get; set; }

        [JsonProperty("max_tables")]
        public long MaxTables { get; set; }
    }
}
