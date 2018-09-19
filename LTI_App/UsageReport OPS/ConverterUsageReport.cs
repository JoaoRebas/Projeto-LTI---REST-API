using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace LTI_App.UsageReport_OPS
{
    internal static class ConverterUsageReport
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
