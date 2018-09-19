using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.Networks_OPS
{
    public class Network
    {
        public object bridge { get; set; }
        public object vpn_public_port { get; set; }
        public object dhcp_start { get; set; }
        public object bridge_interface { get; set; }
        public object share_address { get; set; }
        public object updated_at { get; set; }
        public string id { get; set; }
        public object cidr_v6 { get; set; }
        public object deleted_at { get; set; }
        public object gateway { get; set; }
        public object rxtx_base { get; set; }
        public string label { get; set; }
        public object priority { get; set; }
        public object project_id { get; set; }
        public object vpn_private_address { get; set; }
        public object deleted { get; set; }
        public object vlan { get; set; }
        public object broadcast { get; set; }
        public object netmask { get; set; }
        public object injected { get; set; }
        public object cidr { get; set; }
        public object vpn_public_address { get; set; }
        public object multi_host { get; set; }
        public object enable_dhcp { get; set; }
        public object dns2 { get; set; }
        public object created_at { get; set; }
        public object host { get; set; }
        public object mtu { get; set; }
        public object gateway_v6 { get; set; }
        public object netmask_v6 { get; set; }
        public object dhcp_server { get; set; }
        public object dns1 { get; set; }
    }
}
