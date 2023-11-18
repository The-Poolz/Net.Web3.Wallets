using Net.Web3.EthereumWallet.Extensions;

namespace Net.Web3.EthereumWallet;

public sealed class EthereumAddress : IEquatable<EthereumAddress>, ICloneable
{
    public static readonly string ZeroAddress = "0x0000000000000000000000000000000000000000";
    public string Address { get; }

    public EthereumAddress(string address)
    {
        if (!address.IsValidEthereumAddressHexFormat())
        {
            throw new ArgumentException($"Ethereum address '{address}' is invalid.", nameof(address));
        }

        Address = address;
    }

    public override string ToString()
    {
        return Address;
    }

    #region Implicit conversion operators
    public static implicit operator string(EthereumAddress ethereumAddress)
    {
        return ethereumAddress.Address;
    }

    public static implicit operator EthereumAddress(string address)
    {
        return new EthereumAddress(address);
    }
    #endregion

    #region IEquatable
    public bool Equals(EthereumAddress? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Address == other.Address;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((EthereumAddress)obj);
    }

    public override int GetHashCode()
    {
        return Address.GetHashCode();
    }
    #endregion

    #region ICloneable
    public object Clone()
    {
        return new EthereumAddress(Address);
    }
    #endregion

    #region Comparison operators
    public static bool operator ==(EthereumAddress left, EthereumAddress right)
    {
        return ReferenceEquals(left, right) || left.Equals(right);
    }

    public static bool operator !=(EthereumAddress left, EthereumAddress right)
    {
        return left.Address != right.Address;
    }
    #endregion
}