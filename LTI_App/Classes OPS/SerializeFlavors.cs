using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    public static class SerializeFlavors
    {
        public static string ToJson(this DeserializeFlavors self) => JsonConvert.SerializeObject(self, ConverterFlavors.Settings);
    }
}
