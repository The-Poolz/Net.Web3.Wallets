# Net.Web3.EthereumWallet.Json

## Description
`Net.Web3.EthereumWallet.Json` is a C# library that adds a `Newtonsoft.Json` converter for the `EthereumAddress` class from the `Net.Web3.EthereumWallet` library.

## Installation

You can install the package via NuGet:
```powershell
Install-Package Net.Web3.EthereumWallet.Json
```

Or using .NET CLI:
```powershell
dotnet add package Net.Web3.EthereumWallet.Json
```

## Usage
To use the `Net.Web3.EthereumWallet.Json` converter, you need to configure `Newtonsoft.Json` to recognize and handle the `EthereumAddress` type.
Below are examples demonstrating how to serialize and deserialize objects containing `EthereumAddress` properties.

### Example Scenario
Suppose you have a User class that includes an `EthereumAddress` property:

```csharp
using Newtonsoft.Json;
using Net.Web3.EthereumWallet;

public class User
{
    public string Name { get; set; }

    [JsonConverter(typeof(Net.Web3.EthereumWallet.Json.Converters.EthereumAddressConverter))]
    public EthereumAddress Address { get; set; }
}
```

### Serialization
To serialize a `User` object to JSON, ensuring that the `EthereumAddress` is properly converted to a string:

```csharp
using System;
using Newtonsoft.Json;
using Net.Web3.EthereumWallet;
using Net.Web3.EthereumWallet.Json.Converters;

public class Program
{
    public static void Main()
    {
        var user = new User
        {
            Name = "Alice",
            Address = "0x1234567890abcdef1234567890abcdef12345678"
        };

        string json = JsonConvert.SerializeObject(user, Formatting.Indented);
        Console.WriteLine(json);
    }
}
```

#### Output:
```json
{
  "Name": "Alice",
  "Address": "0x1234567890abcdef1234567890abcdef12345678"
}
```

### Deserialization
To deserialize JSON back into a `User` object, ensuring that the `Address` string is correctly converted to an `EthereumAddress` instance:

```csharp
using System;
using Newtonsoft.Json;
using Net.Web3.EthereumWallet;
using Net.Web3.EthereumWallet.Json.Converters;

public class Program
{
    public static void Main()
    {
        var json = @"{
            ""Name"": ""Bob"",
            ""Address"": ""0xabcdefabcdefabcdefabcdefabcdefabcdefabcd""
        }";

        var user = JsonConvert.DeserializeObject<User>(json)!;

        Console.WriteLine($"Deserialized UserName: {user.Name}");
        Console.WriteLine($"Deserialized UserAddress: {user.Address.Address}");
    }
}
```

#### Output:
```
Deserialized User Name: Bob
Deserialized User Address: 0xabcdefabcdefabcdefabcdefabcdefabcdefabcd
```

## License
`Net.Web3.EthereumWallet.Json` is open-sourced software licensed under the [MIT License](https://github.com/The-Poolz/Net.Web3.EthereumWallet/blob/master/LICENSE).

