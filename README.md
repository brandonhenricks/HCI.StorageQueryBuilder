# HCI.StorageQueryBuilder

StorageQueryBuilder is a .net standard library for quickly building an Azure Storage TableQuery.

![.NET Core](https://github.com/brandonhenricks/HCI.StorageQueryBuilder/workflows/.NET%20Core/badge.svg)

## Installation

Use the package manager [nuget](https://nuget.org) to install StorageQueryBuilder.

```csharp
Install-Package HCI.StorageQueryBuilder
```

## Usage

```csharp
using HCI.StorageQueryBuilder;

// Create a TableQuery with no filters and returning only the PartitionKey
var builder = new QueryBuilder()
    .Select(new List<string>() { "PartitionKey" })
    .Build()
    .ToTableQuery();
```

```csharp
using HCI.StorageQueryBuilder;

// Create a TableQuery with a filter on PartitionKey and returning only the PartitionKey data.
var builder = new QueryBuilder()
    .AddFilter("PartitionKey", "test")
    .AddFilter("RowKey", "000-000-000")
    .Select(new List<string>() { "PartitionKey" });
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.
