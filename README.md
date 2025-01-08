# Net.Web3.Wallets

## Overview
Welcome to the **Net.Web3.Wallets** repository!

`Net.Web3.EthereumWallet` provides the core functionality for validating, representing, and manipulating Ethereum addresses.  
`Net.Web3.EthereumWallet.Json` extends this functionality by providing a dedicated `Newtonsoft.Json` converter for seamless serialization and deserialization of `EthereumAddress` objects.

## Libraries

### Net.Web3.EthereumWallet
- **Description**: A C# library for handling Ethereum addresses in .NET applications.  
- **Key Features**:  
  - Validate Ethereum addresses and confirm format correctness.  
  - Provide easy conversions (implicit from string, back to string, and equality checks).  
  - Format shortened “display” versions of addresses (e.g., `0x1234...7890`).  
- **[Read More](https://github.com/The-Poolz/Net.Web3.EthereumWallet/blob/master/README.md)**

### Net.Web3.EthereumWallet.Json
- **Description**: A C# library that integrates with `Newtonsoft.Json` to serialize and deserialize `EthereumAddress` objects defined in **Net.Web3.EthereumWallet**.
- **Key Features**:  
  - Provides a custom `JsonConverter` so that `EthereumAddress` properties are automatically handled.
- **[Read More](https://github.com/The-Poolz/Net.Web3.EthereumWallet/blob/master/README.Json.md)**
