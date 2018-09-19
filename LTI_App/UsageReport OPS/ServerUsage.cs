using Newtonsoft.Json;
using System;

namespace LTI_App.UsageReport_OPS
{
    public class ServerUsage
    {
        [JsonProperty("instance_id")]
        public string InstanceId { get; set; }

        [JsonProperty("uptime")]
        public long Uptime { get; set; }

        [JsonProperty("started_at")]
        public DateTimeOffset StartedAt { get; set; }

        [JsonProperty("ended_at")]
        public object EndedAt { get; set; }

        [JsonProperty("memory_mb")]
        public long MemoryMb { get; set; }

        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("hours")]
        public double Hours { get; set; }

        [JsonProperty("vcpus")]
        public long Vcpus { get; set; }

        [JsonProperty("flavor")]
        public string Flavor { get; set; }

        [JsonProperty("local_gb")]
        public long LocalGb { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
