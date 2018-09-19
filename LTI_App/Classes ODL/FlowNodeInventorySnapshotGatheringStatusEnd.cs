using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class FlowNodeInventorySnapshotGatheringStatusEnd
    {
        [JsonProperty("end")]
        public DateTimeOffset End { get; set; }

        [JsonProperty("succeeded")]
        public bool Succeeded { get; set; }
    }
}
