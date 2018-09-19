using Newtonsoft.Json;

namespace LTI_App.Instances_OPS
{
    public static class SerializeServer
    {
        
            public static string ToJson(this DeserializeServer self) => JsonConvert.SerializeObject(self, ConverterServer.Settings);
        
    }
}
