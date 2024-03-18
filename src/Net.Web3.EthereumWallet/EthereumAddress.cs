using System;
using Net.Web3.EthereumWallet.Extensions;

namespace Net.Web3.EthereumWallet
{
    /// <summary>
    /// Represents a strongly-typed Ethereum address.
    /// </summary>
    public sealed class EthereumAddress : IEquatable<EthereumAddress>, ICloneable
    {
        /// <summary>
        /// Represents the zero address (0x0) in Ethereum.
        /// </summary>
        public static readonly string ZeroAddress = "0x0000000000000000000000000000000000000000";

        /// <summary>
        /// Gets the Ethereum address as a string.
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EthereumAddress"/> class.
        /// </summary>
        /// <param name="address">The Ethereum address as a hex string.</param>
        /// <exception cref="ArgumentException">Thrown if the address is not in a valid Ethereum hex format.</exception>
        public EthereumAddress(string address)
        {
            if (!address.IsValidEthereumAddressHexFormat())
            {
                throw new ArgumentException($"Ethereum address '{address}' is invalid.", nameof(address));
            }

            Address = address;
        }

        /// <summary>
        /// Returns the string representation of the Ethereum address.
        /// </summary>
        /// <returns>The Ethereum address as a string.</returns>
        public override string ToString()
        {
            return Address;
        }

        /// <summary>
        /// Shortens an Ethereum address by retaining only a specified number of characters at the beginning and end of the address, 
        /// and replacing the middle part with ellipses (...). This method is useful for displaying a concise version of Ethereum addresses.
        /// <example>
        /// <code>
        /// string ethAddress = "0x0000000000000000000000000000000000000000";
        /// string shortAddress = ethAddress.ToShortAddress(4); // Outputs "0x0000...0000"
        /// </code>
        /// </example>
        /// </summary>
        /// <param name="visibleChars">The number of characters to show at the beginning and end of the address after the '0x' prefix. Defaults to 4.</param>
        /// <remarks>
        /// The method ensures that the '0x' prefix and the specified number of characters from both the start and the end of the address are visible.
        /// If the address length is shorter than the expected format, or if the visible characters exceed the address length, the original address is returned.
        /// </remarks>
        /// <returns>
        /// A shortened version of the Ethereum address string with the '0x' prefix, the specified number of characters visible at the start and end, 
        /// and the middle part replaced with ellipses (...).
        /// </returns>
        public string ToShortAddress(byte visibleChars = 4) => Address.ToShortAddress(visibleChars);

        #region Implicit conversion operators
        /// <summary>
        /// Implicitly converts an <see cref="EthereumAddress"/> instance to its string representation.
        /// </summary>
        /// <param name="ethereumAddress">The EthereumAddress instance.</param>
        public static implicit operator string(EthereumAddress ethereumAddress)
        {
            return ethereumAddress.Address;
        }

        /// <summary>
        /// Implicitly converts a string representation of an Ethereum address to an <see cref="EthereumAddress"/> instance.
        /// </summary>
        /// <param name="address">The Ethereum address as a string.</param>
        public static implicit operator EthereumAddress(string address)
        {
            return new EthereumAddress(address);
        }
        #endregion

        #region IEquatable
        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(EthereumAddress? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Address == other.Address;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((EthereumAddress)obj);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return Address.GetHashCode();
        }
        #endregion

        #region ICloneable
        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        public object Clone()
        {
            return new EthereumAddress(Address);
        }
        #endregion

        #region Comparison operators
        /// <summary>
        /// Compares two EthereumAddress instances for equality.
        /// </summary>
        /// <param name="left">The first EthereumAddress instance.</param>
        /// <param name="right">The second EthereumAddress instance.</param>
        /// <returns>true if the instances are equal; otherwise, false.</returns>
        public static bool operator ==(EthereumAddress left, EthereumAddress right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Compares two EthereumAddress instances for inequality.
        /// </summary>
        /// <param name="left">The first EthereumAddress instance.</param>
        /// <param name="right">The second EthereumAddress instance.</param>
        /// <returns>true if the instances are not equal; otherwise, false.</returns>
        public static bool operator !=(EthereumAddress left, EthereumAddress right)
        {
            return !left.Equals(right);
        }
        #endregion
    }
}