using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public partial class Deserialize
    {
        [JsonProperty("links")]
        public WelcomeLinks Links { get; set; }

        [JsonProperty("projects")]
        public List<Project> Projects { get; set; }

        [JsonProperty("servers")]
        public List<Server> Servers { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }
    }

    public partial class Deserialize
    {
        public static Deserialize FromJson(string json) => JsonConvert.DeserializeObject<Deserialize>(json, Converter.Settings);
    }
}
