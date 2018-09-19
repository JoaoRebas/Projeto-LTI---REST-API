using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Node
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("node-connector")]
        public List<NodeConnector> NodeConnector { get; set; }

        [JsonProperty("flow-node-inventory:switch-features")]
        public FlowNodeInventorySwitchFeatures FlowNodeInventorySwitchFeatures { get; set; }

        [JsonProperty("flow-node-inventory:manufacturer")]
        public string FlowNodeInventoryManufacturer { get; set; }

        [JsonProperty("flow-node-inventory:ip-address")]
        public string FlowNodeInventoryIpAddress { get; set; }

        [JsonProperty("flow-node-inventory:software")]
        public string FlowNodeInventorySoftware { get; set; }

        [JsonProperty("flow-node-inventory:serial-number")]
        public string FlowNodeInventorySerialNumber { get; set; }

        [JsonProperty("flow-node-inventory:port-number")]
        public long FlowNodeInventoryPortNumber { get; set; }

        [JsonProperty("flow-node-inventory:table")]
        public List<FlowNodeInventoryTable> FlowNodeInventoryTable { get; set; }

        [JsonProperty("flow-node-inventory:hardware")]
        public string FlowNodeInventoryHardware { get; set; }

        [JsonProperty("flow-node-inventory:description")]
        public string FlowNodeInventoryDescription { get; set; }

        [JsonProperty("flow-node-inventory:snapshot-gathering-status-start")]
        public FlowNodeInventorySnapshotGatheringStatusStart FlowNodeInventorySnapshotGatheringStatusStart { get; set; }

        [JsonProperty("flow-node-inventory:snapshot-gathering-status-end")]
        public FlowNodeInventorySnapshotGatheringStatusEnd FlowNodeInventorySnapshotGatheringStatusEnd { get; set; }
    }
}
