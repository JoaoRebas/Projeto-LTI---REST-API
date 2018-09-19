using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Image
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("links")]
        public List<LinkImage> Links { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
        public string String { get; internal set; }
    }
}
