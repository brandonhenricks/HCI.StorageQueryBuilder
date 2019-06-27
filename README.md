# HCI.StorageQueryBuilder

StorageQueryBuilder is a .net standard library for quickly building an Azure Storage TableQuery.

## Installation

Use the package manager [nuget](https://nuget.org) to install StorageQueryBuilder.

```csharp
Install-Package HCI.StorageQueryBuilder
```

## Usage

```csharp
using HCI.StorageQueryBuilder;

var builder = new QueryBuilder()
    .Select(new List<string>() { "PartitionKey" })
    .Build()
    .ToTableQuery();

```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
