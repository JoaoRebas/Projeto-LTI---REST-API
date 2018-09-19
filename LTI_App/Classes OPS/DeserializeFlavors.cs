using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public partial class DeserializeFlavors
    {
        [JsonProperty("flavors")]
        public List<Flavor> Flavors { get; set; }
    }

    public partial class DeserializeFlavors
    {
        public static DeserializeFlavors FromJson(string json) => JsonConvert.DeserializeObject<DeserializeFlavors>(json, ConverterFlavors.Settings);
    }
}
