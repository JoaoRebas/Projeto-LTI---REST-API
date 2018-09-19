using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public class ApplyActions
    {
        [JsonProperty("action")]
        public List<Action> Action { get; set; }
    }
}
