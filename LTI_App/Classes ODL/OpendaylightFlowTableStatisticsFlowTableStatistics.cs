using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class OpendaylightFlowTableStatisticsFlowTableStatistics
    {
        [JsonProperty("active-flows")]
        public long ActiveFlows { get; set; }

        [JsonProperty("packets-looked-up")]
        public long PacketsLookedUp { get; set; }

        [JsonProperty("packets-matched")]
        public long PacketsMatched { get; set; }
    }
}
