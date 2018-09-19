using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public  class FlowNodeInventoryTable
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("opendaylight-flow-table-statistics:flow-table-statistics")]
        public OpendaylightFlowTableStatisticsFlowTableStatistics OpendaylightFlowTableStatisticsFlowTableStatistics { get; set; }

        [JsonProperty("flow", NullValueHandling = NullValueHandling.Ignore)]
        public List<Flow> Flow { get; set; }
    }
}
