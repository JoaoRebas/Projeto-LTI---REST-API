using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.UsageReport_OPS
{
    public partial class DeserializeUsageReport
    {
        [JsonProperty("tenant_usage")]
        public TenantUsage TenantUsage { get; set; }
    }

    public partial class DeserializeUsageReport
    {
        public static DeserializeUsageReport FromJson(string json) => JsonConvert.DeserializeObject<DeserializeUsageReport>(json, ConverterUsageReport.Settings);
    }

}
