using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class ProjectLinks
    {
        [JsonProperty("self")]
        public string Self { get; set; }
    }
}
