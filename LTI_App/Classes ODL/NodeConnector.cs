using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class NodeConnector
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("flow-node-inventory:state")]
        public FlowNodeInventoryState FlowNodeInventoryState { get; set; }

        [JsonProperty("flow-node-inventory:name")]
        public string FlowNodeInventoryName { get; set; }

        [JsonProperty("flow-node-inventory:maximum-speed")]
        public long FlowNodeInventoryMaximumSpeed { get; set; }

        [JsonProperty("flow-node-inventory:configuration")]
        public FlowNodeInventoryAdvertisedFeatures FlowNodeInventoryConfiguration { get; set; }

        [JsonProperty("flow-node-inventory:supported")]
        public FlowNodeInventoryAdvertisedFeatures FlowNodeInventorySupported { get; set; }

        [JsonProperty("flow-node-inventory:peer-features")]
        public FlowNodeInventoryAdvertisedFeatures FlowNodeInventoryPeerFeatures { get; set; }

        [JsonProperty("flow-node-inventory:port-number")]
        public long FlowNodeInventoryPortNumber { get; set; }

        [JsonProperty("flow-node-inventory:advertised-features")]
        public FlowNodeInventoryAdvertisedFeatures FlowNodeInventoryAdvertisedFeatures { get; set; }

        [JsonProperty("flow-node-inventory:current-feature")]
        public FlowNodeInventoryCurrentFeature FlowNodeInventoryCurrentFeature { get; set; }

        [JsonProperty("flow-node-inventory:hardware-address")]
        public string FlowNodeInventoryHardwareAddress { get; set; }

        [JsonProperty("flow-node-inventory:current-speed")]
        public long FlowNodeInventoryCurrentSpeed { get; set; }

        [JsonProperty("opendaylight-port-statistics:flow-capable-node-connector-statistics")]
        public OpendaylightPortStatisticsFlowCapableNodeConnectorStatistics OpendaylightPortStatisticsFlowCapableNodeConnectorStatistics { get; set; }

        [JsonProperty("stp-status-aware-node-connector:status", NullValueHandling = NullValueHandling.Ignore)]
        public string StpStatusAwareNodeConnectorStatus { get; set; }

        [JsonProperty("address-tracker:addresses", NullValueHandling = NullValueHandling.Ignore)]
        public List<AddressTrackerAddress> AddressTrackerAddresses { get; set; }
    }
}
