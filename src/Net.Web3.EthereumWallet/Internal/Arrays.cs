namespace Net.Web3.EthereumWallet.Internal
{
    internal abstract class Arrays
    {
        internal static void Fill(byte[] buf, byte b)
        {
            var i = buf.Length;
            while (i > 0)
            {
                buf[--i] = b;
            }
        }
    }
}
