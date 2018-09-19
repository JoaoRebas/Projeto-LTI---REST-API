using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace LTI_App.UsageReport_OPS
{
    public class TenantUsage
    {
        [JsonProperty("total_memory_mb_usage")]
        public double TotalMemoryMbUsage { get; set; }

        [JsonProperty("total_vcpus_usage")]
        public double TotalVcpusUsage { get; set; }

        [JsonProperty("start")]
        public DateTimeOffset Start { get; set; }

        [JsonProperty("tenant_id")]
        public string TenantId { get; set; }

        [JsonProperty("stop")]
        public DateTimeOffset Stop { get; set; }

        [JsonProperty("server_usages")]
        public List<ServerUsage> ServerUsages { get; set; }

        [JsonProperty("total_hours")]
        public double TotalHours { get; set; }

        [JsonProperty("total_local_gb_usage")]
        public double TotalLocalGbUsage { get; set; }
    }
}
