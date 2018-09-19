using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace LTI_App.Instances_OPS
{
    internal static class ConverterServer
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
