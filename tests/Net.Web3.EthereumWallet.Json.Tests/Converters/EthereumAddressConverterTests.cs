using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using Net.Web3.EthereumWallet.Json.Converters;

namespace Net.Web3.EthereumWallet.Json.Tests.Converters;

public class EthereumAddressConverterTests
{
    public class Integration
    {
        internal class TestInput
        {
            [JsonConverter(typeof(EthereumAddressConverter))]
            public EthereumAddress UserAddress { get; set; } = EthereumAddress.ZeroAddress;

            [JsonConverter(typeof(EthereumAddressConverter))]
            public EthereumAddress? NullableUserAddress { get; set; } = null;
        }

        internal static string JsonStringOfTestInput => @"{""UserAddress"":""0x0000000000000000000000000000000000000000"",""NullableUserAddress"":null}";

        [Fact]
        internal void WriteJson_ShouldReceiveExpectedJsonString()
        {
            var obj = new TestInput();

            var stringJson = JsonConvert.SerializeObject(obj, Formatting.None);

            stringJson.Should().Be(JsonStringOfTestInput);
        }

        [Fact]
        internal void ReadJson_ShouldReceiveExpectedObject()
        {
            var stringJson = JsonStringOfTestInput;

            var obj = JsonConvert.DeserializeObject<TestInput>(stringJson);

            var expected = new TestInput();
            obj.Should().BeEquivalentTo(expected);
        }
    }
}