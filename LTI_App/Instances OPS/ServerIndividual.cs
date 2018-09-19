using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.Instances_OPS
{
    public class ServerIndividual
    {
        
            [JsonProperty("OS-EXT-STS:task_state")]
            public object OsExtStsTaskState { get; set; }

            [JsonProperty("addresses")]
            public Addresses Addresses { get; set; }

            [JsonProperty("links")]
            public List<Link> Links { get; set; }

            [JsonProperty("image")]
            public Flavor Image { get; set; }

            [JsonProperty("OS-EXT-STS:vm_state")]
            public string OsExtStsVmState { get; set; }

            [JsonProperty("OS-EXT-SRV-ATTR:instance_name")]
            public string OsExtSrvAttrInstanceName { get; set; }

            [JsonProperty("OS-SRV-USG:launched_at")]
            public DateTimeOffset OsSrvUsgLaunchedAt { get; set; }

            [JsonProperty("flavor")]
            public Flavor Flavor { get; set; }

            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("security_groups")]
            public List<SecurityGroup> SecurityGroups { get; set; }

            [JsonProperty("user_id")]
            public string UserId { get; set; }

            [JsonProperty("OS-DCF:diskConfig")]
            public string OsDcfDiskConfig { get; set; }

            [JsonProperty("accessIPv4")]
            public string AccessIPv4 { get; set; }

            [JsonProperty("accessIPv6")]
            public string AccessIPv6 { get; set; }

            [JsonProperty("progress")]
            public long Progress { get; set; }

            [JsonProperty("OS-EXT-STS:power_state")]
            public long OsExtStsPowerState { get; set; }

            [JsonProperty("OS-EXT-AZ:availability_zone")]
            public string OsExtAzAvailabilityZone { get; set; }

            [JsonProperty("config_drive")]
            public string ConfigDrive { get; set; }

            [JsonProperty("status")]
            public string Status { get; set; }

            [JsonProperty("updated")]
            public DateTimeOffset Updated { get; set; }

            [JsonProperty("hostId")]
            public string HostId { get; set; }

            [JsonProperty("OS-EXT-SRV-ATTR:host")]
            public string OsExtSrvAttrHost { get; set; }

            [JsonProperty("OS-SRV-USG:terminated_at")]
            public object OsSrvUsgTerminatedAt { get; set; }

            [JsonProperty("key_name")]
            public object KeyName { get; set; }

            [JsonProperty("OS-EXT-SRV-ATTR:hypervisor_hostname")]
            public string OsExtSrvAttrHypervisorHostname { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("created")]
            public DateTimeOffset Created { get; set; }

            [JsonProperty("tenant_id")]
            public string TenantId { get; set; }

            [JsonProperty("os-extended-volumes:volumes_attached")]
            public List<object> OsExtendedVolumesVolumesAttached { get; set; }

            [JsonProperty("metadata")]
            public Metadata Metadata { get; set; }
        
    }
}
