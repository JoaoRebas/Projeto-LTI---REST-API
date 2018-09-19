using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Action
    {
        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("output-action")]
        public OutputAction OutputAction { get; set; }
    }
}
