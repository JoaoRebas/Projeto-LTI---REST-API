using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Instruction
    {
        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("apply-actions")]
        public ApplyActions ApplyActions { get; set; }
    }
}
