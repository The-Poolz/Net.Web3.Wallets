using System.ComponentModel.DataAnnotations;

namespace Net.Web3.EthereumWallet;

public class EthereumAddressAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var address = value as string;
        if (string.IsNullOrWhiteSpace(address))
        {
            return new ValidationResult("Ethereum address is null or empty.");
        }

        return !address.IsValidEthereumAddressHexFormat() ?
            new ValidationResult($"Ethereum address '{address}' is invalid.") :
            ValidationResult.Success;
    }
}