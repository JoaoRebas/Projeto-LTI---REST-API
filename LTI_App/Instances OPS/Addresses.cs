using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.Instances_OPS
{
    public class Addresses
    {
        [JsonProperty("private")]
        public List<Private> Private { get; set; }
    }
}
