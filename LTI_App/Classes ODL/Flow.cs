using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Flow
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("idle-timeout")]
        public long IdleTimeout { get; set; }

        [JsonProperty("cookie")]
        public long Cookie { get; set; }

        [JsonProperty("flags")]
        public FlowNodeInventoryAdvertisedFeatures Flags { get; set; }

        [JsonProperty("hard-timeout")]
        public long HardTimeout { get; set; }

        [JsonProperty("instructions", NullValueHandling = NullValueHandling.Ignore)]
        public Instructions Instructions { get; set; }

        [JsonProperty("cookie_mask")]
        public long CookieMask { get; set; }

        [JsonProperty("opendaylight-flow-statistics:flow-statistics")]
        public OpendaylightFlowStatisticsFlowStatistics OpendaylightFlowStatisticsFlowStatistics { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("table_id")]
        public long TableId { get; set; }

        [JsonProperty("match")]
        public Match Match { get; set; }
    }
}
