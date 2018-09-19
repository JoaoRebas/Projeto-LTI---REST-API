using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Bytes
    {
        [JsonProperty("transmitted")]
        public long Transmitted { get; set; }

        [JsonProperty("received")]
        public long Received { get; set; }
    }
}
