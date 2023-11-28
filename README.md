[![CodeFactor](https://www.codefactor.io/repository/github/the-poolz/net.web3.ethereumwallet/badge)](https://www.codefactor.io/repository/github/the-poolz/net.web3.ethereumwallet)

# Net.Web3.EthereumWallet
## Description
`Net.Web3.EthereumWallet` is a C# library for handling Ethereum addresses. It provides a convenient way to validate, represent, and manipulate Ethereum addresses in .NET applications.

## Installation

You can install the package via NuGet:
```powershell
Install-Package Net.Web3.EthereumWallet
```

Or using .NET CLI:
```powershell
dotnet add package Net.Web3.EthereumWallet
```

## Usage

### Creating and Validating Ethereum Addresses
```csharp
using Net.Web3.EthereumWallet;

// Creating an Ethereum address
var ethAddress = new EthereumAddress("0x1234567890abcdef1234567890abcdef1234567890");

// Validating the address format
bool isValid = ethAddress.Address.IsValidEthereumAddressHexFormat();
```

### Conversion and Comparison
```csharp
// Implicit conversion from a string
EthereumAddress addressFromString = "0x1234567890abcdef1234567890abcdef1234567890";

// Converting back to a string
string addressString = addressFromString;

// Comparing addresses
bool areEqual = addressFromString == new EthereumAddress("0x1234567890abcdef1234567890abcdef1234567890");
```

### Shortened Address Representation
```csharp
// Getting a shortened version of the address
string shortAddress = ethAddress.ToShortAddress(4); // Outputs "0x1234...7890"
```

## License
`Net.Web3.EthereumWallet` is open-sourced software licensed under the [MIT License](https://github.com/The-Poolz/Net.Web3.EthereumWallet/blob/master/LICENSE).

