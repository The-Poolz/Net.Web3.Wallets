namespace Net.Web3.EthereumWallet.Extensions;

/// <summary>
/// Provides extension methods for Ethereum address validation.
/// </summary>
public static class AddressExtensions
{
    /// <summary>
    /// Determines whether a string is a valid Ethereum address in hexadecimal format.
    /// </summary>
    /// <param name="address">The string representation of the Ethereum address.</param>
    /// <returns>true if the address is a valid Ethereum address in hexadecimal format; otherwise, false.</returns>
    public static bool IsValidEthereumAddressHexFormat(this string address)
    {
        if (string.IsNullOrEmpty(address) || !address.HasHexPrefix()) return false;

        address = address[2..];
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
    /// Determines whether a string contains only hexadecimal characters.
    /// </summary>
    /// <param name="value">The string to check.</param>
    /// <returns>true if the string contains only hexadecimal characters; otherwise, false.</returns>
    public static bool IsHex(this string value)
    {
        return value.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F');
    }
}