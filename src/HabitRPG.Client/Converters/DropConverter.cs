using HabitRPG.Client.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;

namespace HabitRPG.Client.Converters
{
    internal class DropConverter : CustomCreationConverter<Drop>
    {
        public override Drop Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public Drop Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("type");

            switch (type)
            {
                case "Food":
                    return new Food();

                case "HatchingPotion":
                    return new HatchingPotion();

                case "Egg":
                    return new Egg();
            }

            throw new Exception(String.Format("Type: {0} not supported", type));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load JObject from stream
            JObject jObject = JObject.Load(reader);

            // Create target object based on JObject
            var target = Create(objectType, jObject);

            // Populate the object properties
            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
