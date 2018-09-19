using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App.Classes_ODL
{
    public static class SerializeSpecNode
    {
        public static string ToJson(this NodeList self) => JsonConvert.SerializeObject(self, ConverterSpecNode.Settings);
    }
}
