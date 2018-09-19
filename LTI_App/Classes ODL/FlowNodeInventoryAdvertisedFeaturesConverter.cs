using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    internal class FlowNodeInventoryAdvertisedFeaturesConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FlowNodeInventoryAdvertisedFeatures) || t == typeof(FlowNodeInventoryAdvertisedFeatures?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "")
            {
                return FlowNodeInventoryAdvertisedFeatures.Empty;
            }
            throw new Exception("Cannot unmarshal type FlowNodeInventoryAdvertisedFeatures");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (FlowNodeInventoryAdvertisedFeatures)untypedValue;
            if (value == FlowNodeInventoryAdvertisedFeatures.Empty)
            {
                serializer.Serialize(writer, ""); return;
            }
            throw new Exception("Cannot marshal type FlowNodeInventoryAdvertisedFeatures");
        }
    }
}
