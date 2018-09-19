using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Globalization;

namespace LTI_App.Networks_OPS
{
    internal static class ConverterNetworks
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
