using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ.Data
{
    public class KeysJsonConverter : JsonConverter
    {
        private readonly Type[] _types;

        public KeysJsonConverter(params Type[] types)
        {
            _types = types;
        }
        public override bool CanConvert(Type objectType)
        {
            return _types.Any(t => t == objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Newtonsoft.Json.Linq.JToken t = JToken.FromObject(value);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartObject();

            JObject o = (JObject)t;
            IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();
            // List<JToken> jts = ((Newtonsoft.Json.Linq.JObject)t).Children;  writer.WriteValue("\n");
            foreach (string p in propertyNames)
            {
                writer.Formatting = Formatting.None;
                writer.WritePropertyName(p);
                var s = value.GetType().GetProperty(p).GetValue(value, null); ;
                writer.WriteValue(s);
            }

            // if (t.Type != JTokenType.Object)
            // {
            //   t.WriteTo(writer);
            // }
            //else
            //{
            //    JObject o = (JObject)t;
            //    IList<string> propertyNames = o.Properties().Select(p => p.Name).ToList();

            //    o.AddFirst(new JProperty("Keys", new JArray(propertyNames)));

            //    o.WriteTo(writer);
            //}
            writer.WriteEndObject();
            writer.CloseOutput = false;


        }
    }
}
