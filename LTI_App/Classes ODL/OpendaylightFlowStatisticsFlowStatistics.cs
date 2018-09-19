using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class OpendaylightFlowStatisticsFlowStatistics
    {
        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("byte-count")]
        public long ByteCount { get; set; }

        [JsonProperty("packet-count")]
        public long PacketCount { get; set; }
    }
}
