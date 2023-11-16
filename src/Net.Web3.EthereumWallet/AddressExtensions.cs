namespace Net.Web3.EthereumWallet;

public static class AddressExtensions
{
    public static bool IsValidEthereumAddressHexFormat(this string address)
    {
        if (string.IsNullOrEmpty(address) || !address.HasHexPrefix()) return false;

        address = address[2..];
        return address.Length == 40 && address.IsHex();
    }

    public static bool HasHexPrefix(this string value)
    {
        return value.StartsWith("0x");
    }

    public static bool IsHex(this string value)
    {
        return value.All(c => c is >= '0' and <= '9' or >= 'a' and <= 'f' or >= 'A' and <= 'F');
    }
}