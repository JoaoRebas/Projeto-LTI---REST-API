using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class OpendaylightPortStatisticsFlowCapableNodeConnectorStatistics
    {
        [JsonProperty("receive-errors")]
        public long ReceiveErrors { get; set; }

        [JsonProperty("packets")]
        public Bytes Packets { get; set; }

        [JsonProperty("receive-over-run-error")]
        public long ReceiveOverRunError { get; set; }

        [JsonProperty("transmit-drops")]
        public long TransmitDrops { get; set; }

        [JsonProperty("collision-count")]
        public long CollisionCount { get; set; }

        [JsonProperty("receive-frame-error")]
        public long ReceiveFrameError { get; set; }

        [JsonProperty("bytes")]
        public Bytes Bytes { get; set; }

        [JsonProperty("receive-drops")]
        public long ReceiveDrops { get; set; }

        [JsonProperty("transmit-errors")]
        public long TransmitErrors { get; set; }

        [JsonProperty("duration")]
        public Duration Duration { get; set; }

        [JsonProperty("receive-crc-error")]
        public long ReceiveCrcError { get; set; }
    }
}
