using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class FlowNodeInventoryState
    {
        [JsonProperty("link-down")]
        public bool LinkDown { get; set; }

        [JsonProperty("live")]
        public bool Live { get; set; }

        [JsonProperty("blocked")]
        public bool Blocked { get; set; }
    }
}
