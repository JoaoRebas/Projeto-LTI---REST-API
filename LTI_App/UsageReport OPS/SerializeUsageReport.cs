using Newtonsoft.Json;

namespace LTI_App.UsageReport_OPS
{
    public static class SerializeUsageReport
    {
        public static string ToJson(this DeserializeUsageReport self) => JsonConvert.SerializeObject(self, ConverterUsageReport.Settings);
    }
}
