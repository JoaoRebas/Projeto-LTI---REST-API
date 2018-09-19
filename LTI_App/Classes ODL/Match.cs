using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Match
    {
        [JsonProperty("in-port", NullValueHandling = NullValueHandling.Ignore)]
        public string InPort { get; set; }

        [JsonProperty("ethernet-match", NullValueHandling = NullValueHandling.Ignore)]
        public EthernetMatch EthernetMatch { get; set; }
    }
}
