using Newtonsoft.Json;

namespace LTI_App.Networks_OPS
{
    public static class SerializeNetworks
    {
        
            public static string ToJson(this DeserializeNetworks self) => JsonConvert.SerializeObject(self, ConverterNetworks.Settings);
        
    }
}
