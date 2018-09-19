using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    internal class FlowNodeInventoryCurrentFeatureConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(FlowNodeInventoryCurrentFeature) || t == typeof(FlowNodeInventoryCurrentFeature?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return FlowNodeInventoryCurrentFeature.Empty;
                case "ten-gb-fd copper":
                    return FlowNodeInventoryCurrentFeature.TenGbFdCopper;
            }
            throw new Exception("Cannot unmarshal type FlowNodeInventoryCurrentFeature");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (FlowNodeInventoryCurrentFeature)untypedValue;
            switch (value)
            {
                case FlowNodeInventoryCurrentFeature.Empty:
                    serializer.Serialize(writer, ""); return;
                case FlowNodeInventoryCurrentFeature.TenGbFdCopper:
                    serializer.Serialize(writer, "ten-gb-fd copper"); return;
            }
            throw new Exception("Cannot marshal type FlowNodeInventoryCurrentFeature");
        }
    }
}
