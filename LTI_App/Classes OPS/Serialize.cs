using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public static class Serialize
    {
        public static string ToJson(this Deserialize self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }
}
