using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public partial class WelcomeLinks
    {
        [JsonProperty("self")]
        public string Self { get; set; }

        [JsonProperty("previous")]
        public object Previous { get; set; }

        [JsonProperty("next")]
        public object Next { get; set; }
    }
}
