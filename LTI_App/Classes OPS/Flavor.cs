using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Flavor
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("links")]
        public List<LinkFlavor> Links { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
