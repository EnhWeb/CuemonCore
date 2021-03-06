﻿using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Cuemon.Serialization.Json.Converters
{
    /// <summary>
    /// Converts a FlagsEnum to its name string value.
    /// </summary>
    /// <seealso cref="StringEnumConverter" />
    public class StringFlagsEnumConverter : StringEnumConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringFlagsEnumConverter"/> class.
        /// </summary>
        public StringFlagsEnumConverter() : base(true)
        {
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The <see cref="T:Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            var e = (Enum)value;
            var enumName = e.ToString("G");
            var flags = enumName.Split(',');
            var enumType = e.GetType();
            if (enumType.GetTypeInfo().IsDefined(typeof(FlagsAttribute), false))
            {
                writer.WriteStartArray();
                foreach (var flag in flags)
                {
                    writer.WriteValue(Condition.TernaryIf(CamelCaseText, () => StringConverter.ToCamelCasing(flag.Trim()), () => flag.Trim()));
                }
                writer.WriteEndArray();
            }
            else
            {
                if (char.IsNumber(enumName[0]) || enumName[0] == '-')
                {
                    writer.WriteValue(value);
                }
                else
                {
                    writer.WriteValue(Condition.TernaryIf(CamelCaseText, () => StringConverter.ToCamelCasing(enumName), () => enumName));
                }
            }
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T:Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            int result = 0;
            while (reader.Read())
            {
                switch (reader.TokenType)
                {
                    case JsonToken.String:
                        result |= (int)Enum.Parse(objectType, reader.Value.ToString(), true);
                        break;
                }
            }
            return result;
        }
    }
}