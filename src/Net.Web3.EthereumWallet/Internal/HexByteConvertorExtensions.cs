using System.Linq;

namespace Net.Web3.EthereumWallet.Internal
{
    internal static class HexByteConvertorExtensions
    {
        internal static string ToHex(this byte[] value, bool prefix = false)
        {
            var strPrefix = prefix ? "0x" : "";
            return strPrefix + string.Concat(value.Select(b => b.ToString("x2")).ToArray());
        }
    }
}
