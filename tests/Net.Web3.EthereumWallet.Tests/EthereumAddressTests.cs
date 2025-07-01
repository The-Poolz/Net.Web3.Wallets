using Xunit;
using FluentAssertions;
using static Net.Web3.EthereumWallet.EthereumAddress;

namespace Net.Web3.EthereumWallet.Tests;

public class EthereumAddressTests
{
    [Fact]
    internal void InvalidAddress_InvalidLength_ThrowException()
    {
        var address = "0x" + new string('1', 41);

        Action testCode = () => _ = new EthereumAddress(address);

        testCode.Should().Throw<ArgumentException>().WithMessage($"Ethereum address '{address}' is invalid. (Parameter 'address')");
    }

    [Fact]
    internal void InvalidAddress_WithoutHexPrefix_ThrowException()
    {
        var address = ZeroAddress[2..];

        Action testCode = () => _ = new EthereumAddress(address);

        testCode.Should().Throw<ArgumentException>().WithMessage($"Ethereum address '{address}' is invalid. (Parameter 'address')");
    }

    [Fact]
    internal void ImplicitConversion_FromStringToEthereumAddress()
    {
        EthereumAddress address = ZeroAddress;

        address.Address.Should().Be(ZeroAddress);
    }

    [Fact]
    internal void ImplicitConversion_FromEthereumAddressToString()
    {
        string address = new EthereumAddress(ZeroAddress);

        address.Should().Be(ZeroAddress);
    }

    [Fact]
    internal void ToString_ExpectedAddress()
    {
        var address = new EthereumAddress(ZeroAddress);

        address.ToString().Should().Be(ZeroAddress);
    }

    [Fact]
    internal void ToShortAddress_ExpectedAddress()
    {
        Action testCode = () => new EthereumAddress(ZeroAddress).ToShortAddress(16);

        testCode.Should().Throw<ArgumentException>().WithMessage("Visible characters cannot be less than 1 or greater than 15. (Parameter 'visibleChars')");
    }

    [Fact]
    internal void ToShortAddress_ThrowException()
    {
        var address = new EthereumAddress(ZeroAddress).ToShortAddress(3);

        address.Should().Be("0x000...000");
    }

    [Fact]
    internal void Equals_ForEthereumAddress_IsEquals()
    {
        var address1 = new EthereumAddress(ZeroAddress);
        var address2 = new EthereumAddress(ZeroAddress);

        var result = address1.Equals(address2);

        result.Should().BeTrue();
    }

    [Fact]
    internal void Equals_ForEthereumAddress_NotEquals()
    {
        var address1 = new EthereumAddress(ZeroAddress);
        var address2 = new EthereumAddress("0x" + new string('1', 40));

        var result = address1.Equals(address2);

        result.Should().BeFalse();
    }

    [Fact]
    internal void Equals_ForEthereumAddress_ReferenceEquals()
    {
        var address1 = new EthereumAddress(ZeroAddress);

        var result = address1.Equals(address1);

        result.Should().BeTrue();
    }

    [Fact]
    internal void Equals_ForEthereumAddress_OtherIsNull()
    {
        var address1 = new EthereumAddress(ZeroAddress);

        var result = address1.Equals(null!);

        result.Should().BeFalse();
    }

    [Fact]
    internal void Equals_ForObject_IsEquals()
    {
        object address1 = new EthereumAddress(ZeroAddress);
        object address2 = new EthereumAddress(ZeroAddress);

        var result = address1.Equals(address2);

        result.Should().BeTrue();
    }

    [Fact]
    internal void Equals_ForObject_NotEquals()
    {
        object address1 = new EthereumAddress(ZeroAddress);
        object address2 = new EthereumAddress("0x" + new string('1', 40));

        var result = address1.Equals(address2);

        result.Should().BeFalse();
    }

    [Fact]
    internal void Equals_ForObject_ReferenceEquals()
    {
        object address1 = new EthereumAddress(ZeroAddress);

        var result = address1.Equals(address1!);

        result.Should().BeTrue();
    }

    [Fact]
    internal void Equals_ForObject_OtherIsNull()
    {
        object address1 = new EthereumAddress(ZeroAddress);

        var result = address1.Equals(null!);

        result.Should().BeFalse();
    }

    [Fact]
    internal void GetHashCode_TheSameAddress()
    {
        object address1 = new EthereumAddress(ZeroAddress);
        object address2 = new EthereumAddress(ZeroAddress);

        address1.GetHashCode().Should().Be(address2.GetHashCode());
    }

    [Fact]
    internal void GetHashCode_DifferentAddress()
    {
        object address1 = new EthereumAddress(ZeroAddress);
        object address2 = new EthereumAddress("0x" + new string('1', 40));

        address1.GetHashCode().Should().NotBe(address2.GetHashCode());
    }

    [Fact]
    internal void Clone_TheSameAddress()
    {
        var address1 = new EthereumAddress(ZeroAddress);
        var address2 = (EthereumAddress)address1.Clone();

        address1.Address.Should().Be(address2.Address);
    }

    [Fact]
    internal void ComparisonOperators_Equals()
    {
        var address1 = new EthereumAddress(ZeroAddress);
        var address2 = (EthereumAddress)address1.Clone();

        var result = address1 == address2;

        result.Should().BeTrue();
    }

    [Fact]
    internal void ComparisonOperators_NotEquals()
    {
        var address1 = new EthereumAddress(ZeroAddress);
        var address2 = (EthereumAddress)address1.Clone();

        var result = address1 != address2;

        result.Should().BeFalse();
    }

    [Fact]
    internal void ComparisonOperators_NullEqualsNull()
    {
        EthereumAddress? address1 = null;
        EthereumAddress? address2 = null;

        var result = address1 == address2;

        result.Should().BeTrue();
    }

    [Fact]
    internal void ComparisonOperators_AddressEqualsNull()
    {
        var address1 = new EthereumAddress(ZeroAddress);
        EthereumAddress? address2 = null;

        var result = address1 == address2;

        result.Should().BeFalse();
    }

    [Fact]
    internal void ComparisonOperators_NullNotEqualsAddress()
    {
        EthereumAddress? address1 = null;
        var address2 = new EthereumAddress(ZeroAddress);

        var result = address1 != address2;

        result.Should().BeTrue();
    }
}