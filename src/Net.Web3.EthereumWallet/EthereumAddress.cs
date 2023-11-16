namespace Net.Web3.EthereumWallet;

public class EthereumAddress
{
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
}