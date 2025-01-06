using System;
using Newtonsoft.Json;

namespace Net.Web3.EthereumWallet.Json.Converters
{
    public class EthereumAddressConverter : JsonConverter<EthereumAddress>
    {
        public override void WriteJson(JsonWriter writer, EthereumAddress? value, JsonSerializer serializer)
        {
            if (value?.Address == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(value.Address);
        }

        public override EthereumAddress ReadJson(JsonReader reader, Type objectType, EthereumAddress? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
            {
                throw new JsonSerializationException($"Cannot convert null value to {nameof(EthereumAddress)}.");
            }

            if (reader.TokenType != JsonToken.String)
            {
                throw new JsonSerializationException($"Cannot convert not string value to {nameof(EthereumAddress)}.");
            }

            return new EthereumAddress((string)reader.Value!);
        }
    }
}
