using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class Instructions
    {
        [JsonProperty("instruction")]
        public List<Instruction> Instruction { get; set; }
    }
}
