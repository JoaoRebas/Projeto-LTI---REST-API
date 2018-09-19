using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTI_App
{
    internal class RelConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Rel) || t == typeof(Rel?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "bookmark":
                    return Rel.Bookmark;
                case "self":
                    return Rel.Self;
            }
            throw new Exception("Cannot unmarshal type Rel");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (Rel)untypedValue;
            switch (value)
            {
                case Rel.Bookmark:
                    serializer.Serialize(writer, "bookmark"); return;
                case Rel.Self:
                    serializer.Serialize(writer, "self"); return;
            }
            throw new Exception("Cannot marshal type Rel");
        }
    }
}
