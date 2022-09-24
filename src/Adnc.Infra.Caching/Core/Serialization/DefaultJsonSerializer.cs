﻿using Adnc.Infra.Caching.Core.Internal;
using Adnc.Infra.Core.Adnc.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Adnc.Infra.Caching.Core.Serialization
{
    /// <summary>
    /// Default json serializer.
    /// </summary>
    public class DefaultJsonSerializer : IRedisSerializer
    {
        /// <summary>
        /// The json serializer.
        /// </summary>
        private static readonly JsonSerializerOptions jsonSerializerOption = SystemTextJson.GetAdncDefaultOptions();

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name => RedisConstValue.Serializer.DefaultJsonSerializerName;

        /// <summary>
        /// Deserialize the specified bytes.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="bytes">Bytes.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Deserialize<T>(byte[] bytes)
        {
            return JsonSerializer.Deserialize<T>(bytes, jsonSerializerOption);
        }
        /// <summary>
        /// Deserialize the specified bytes.
        /// </summary>
        /// <returns>The deserialize.</returns>
        /// <param name="bytes">Bytes.</param>
        /// <param name="type">Type.</param>
        public object Deserialize(byte[] bytes, Type type)
        {
            return JsonSerializer.Deserialize(bytes, type, jsonSerializerOption);
        }
        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="value">Value.</param>
        public object DeserializeObject(ArraySegment<byte> value)
        {
            var jr = new Utf8JsonReader(value);
            jr.Read();
            if (jr.TokenType == JsonTokenType.StartArray)
            {
                jr.Read();
                var typeName = Encoding.UTF8.GetString(jr.ValueSpan.ToArray());
                var type = Type.GetType(typeName, throwOnError: true);

                jr.Read();
                return JsonSerializer.Deserialize(ref jr, type, jsonSerializerOption);
            }
            else
            {
                throw new InvalidDataException("JsonTranscoder only supports [\"TypeName\", object]");
            }
        }

        /// <summary>
        /// Serialize the specified value.
        /// </summary>
        /// <returns>The serialize.</returns>
        /// <param name="value">Value.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public byte[] Serialize<T>(T value)
        {
            return JsonSerializer.SerializeToUtf8Bytes(value, jsonSerializerOption);
        }
        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="obj">Object.</param>
        public ArraySegment<byte> SerializeObject(object obj)
        {
            var typeName = TypeHelper.BuildTypeName(obj.GetType());

            using var ms = new MemoryStream();
            using var jw = new Utf8JsonWriter(ms);
            jw.WriteStartArray();
            jw.WriteStringValue(typeName);

            JsonSerializer.Serialize(jw, obj, jsonSerializerOption);

            jw.WriteEndArray();

            jw.Flush();

            return new ArraySegment<byte>(ms.ToArray(), 0, (int)ms.Length);
        }
    }
}
