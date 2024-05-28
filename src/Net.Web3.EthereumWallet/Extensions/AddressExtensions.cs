using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Globalization;
using Net.Web3.EthereumWallet.Internal;

namespace Net.Web3.EthereumWallet.Extensions
{
    /// <summary>
    /// Provides extension methods for Ethereum address validation.
    /// </summary>
    public static class AddressExtensions
    {
        /// <summary>
        /// Converts an Ethereum address to its checksum address format as per EIP-55, which may involve a chain-specific prefix.
        /// </summary>
        /// <param name="address">The Ethereum address to convert.</param>
        /// <param name="chainId">Optional. The chain ID to use as a prefix in the hash calculation for checksum.</param>
        /// <returns>A checksummed Ethereum address with the '0x' prefix.</returns>
        /// <remarks>
        /// This method calculates the checksum using the SHA3 Keccak hash of the address. Each character in the original
        /// address is then capitalized if the corresponding character in the hash is greater than 7.
        /// </remarks>
        public static string ConvertToChecksumAddress(this string address, BigInteger? chainId = null)
        {
            address = address.ToLower().RemoveHexPrefix();
            var prefix = chainId != null ? chainId + "0x" : "";
            var addressHash = new Sha3Keccack().CalculateHash(prefix + address);
            var checksumAddress = new StringBuilder("0x");

            for (var i = 0; i < address.Length; i++)
            {
                if (int.Parse(addressHash[i].ToString(), NumberStyles.HexNumber) > 7)
                {
                    checksumAddress.Append(address[i].ToString().ToUpper());
                }
                else
                {
                    checksumAddress.Append(address[i]);
                }
            }

            return checksumAddress.ToString();
        }

        /// <inheritdoc cref="ConvertToChecksumAddress(string, BigInteger?)"/>
        public static string ConvertToChecksumAddress(this EthereumAddress address, BigInteger? chainId = null) =>
            ConvertToChecksumAddress(address.Address, chainId);

        /// <summary>
        /// Validates whether a given Ethereum address matches the checksum format as per EIP-55, optionally considering a chain-specific prefix.
        /// </summary>
        /// <param name="address">The Ethereum address to validate.</param>
        /// <param name="chainId">Optional. The chain ID to use as a prefix when computing the checksum for comparison.</param>
        /// <returns>true if the address is in valid checksum format; otherwise, false.</returns>
        /// <remarks>
        /// This method performs checksum validation by recalculating the hash with the same chain prefix used in checksum creation,
        /// then compares each character of the original address against the hash to determine if it matches the checksum format.
        /// </remarks>
        public static bool IsChecksumAddress(this string address, BigInteger? chainId = null)
        {
            if (string.IsNullOrEmpty(address)) return false;
            address = address.RemoveHexPrefix();
            var prefix = chainId != null ? chainId + "0x" : "";
            var addressHash = new Sha3Keccack().CalculateHash(prefix + address.ToLower());

            for (var i = 0; i < address.Length; i++)
            {
                var value = int.Parse(addressHash[i].ToString(), NumberStyles.HexNumber);
                if ((value > 7 && address[i].ToString().ToUpper() != address[i].ToString()) ||
                    (value <= 7 && address[i].ToString().ToLower() != address[i].ToString()))
                    return false;
            }
            return true;
        }

        /// <inheritdoc cref="IsChecksumAddress(string, BigInteger?)"/>
        public static bool IsChecksumAddress(this EthereumAddress address, BigInteger? chainId = null) =>
            IsChecksumAddress(address.Address, chainId);

        /// <summary>
        /// Determines whether a string is a valid Ethereum address in hexadecimal format.
        /// </summary>
        /// <param name="address">The string representation of the Ethereum address.</param>
        /// <returns>true if the address is a valid Ethereum address in hexadecimal format; otherwise, false.</returns>
        public static bool IsValidEthereumAddressHexFormat(this string address)
        {
            if (string.IsNullOrEmpty(address) || !address.HasHexPrefix()) return false;

            address = address.RemoveHexPrefix();
            return address.Length == 40 && address.IsHex();
        }

        /// <summary>
        /// Checks if a string starts with the hexadecimal prefix (0x).
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>true if the string starts with '0x'; otherwise, false.</returns>
        public static bool HasHexPrefix(this string value)
        {
            return value.StartsWith("0x");
        }

        /// <summary>
        /// Removes the '0x' prefix from a hexadecimal string, if present.
        /// </summary>
        /// <param name="value">The hexadecimal string from which to remove the '0x' prefix.</param>
        /// <returns>The hexadecimal string without the '0x' prefix.</returns>
        /// <remarks>
        /// This method checks the beginning of the string for the '0x' prefix and removes it if found, returning the rest of the string.
        /// If the '0x' prefix is not found, the original string is returned unchanged.
        /// </remarks>
        public static string RemoveHexPrefix(this string value)
        {
            return value[(value.StartsWith("0x") ? 2 : 0)..];
        }

        /// <summary>
        /// Determines whether a string contains only hexadecimal characters.
        /// </summary>
        /// <param name="value">The string to check.</param>
        /// <returns>true if the string contains only hexadecimal characters; otherwise, false.</returns>
        public static bool IsHex(this string value)
        {
            return value.All(c => (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'));
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
        /// <param name="address">The Ethereum address string to be shortened. Expected to start with '0x' followed by 40 hexadecimal characters.</param>
        /// <param name="visibleChars">The number of characters to show at the beginning and end of the address after the '0x' prefix. Defaults to 4.</param>
        /// <remarks>
        /// The method ensures that the '0x' prefix and the specified number of characters from both the start and the end of the address are visible.
        /// If the address length is shorter than the expected format, or if the visible characters exceed the address length, the original address is returned.
        /// </remarks>
        /// <returns>
        /// A shortened version of the Ethereum address string with the '0x' prefix, the specified number of characters visible at the start and end, 
        /// and the middle part replaced with ellipses (...).
        /// </returns>
        public static string ToShortAddress(this string address, byte visibleChars = 4)
        {
            if (visibleChars < 1 || visibleChars > 15) throw new ArgumentException("Visible characters cannot be less than 1 or greater than 15.", nameof(visibleChars));

            return $"{address[..(2 + visibleChars)]}...{address[^visibleChars..]}";
        }
    }
}