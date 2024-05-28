using System.Text;

namespace Net.Web3.EthereumWallet.Internal
{
    internal class Sha3Keccack
    {
        internal string CalculateHash(string value)
        {
            var input = Encoding.UTF8.GetBytes(value);
            var output = CalculateHash(input);
            return output.ToHex();
        }

        internal byte[] CalculateHash(byte[] value)
        {
            var digest = new KeccakDigest(256);
            var output = new byte[digest.GetDigestSize()];
            digest.BlockUpdate(value, 0, value.Length);
            digest.DoFinal(output, 0);
            return output;
        }
    }
}
