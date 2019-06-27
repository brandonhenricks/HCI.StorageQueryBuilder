using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
    public struct QueryFilter : IQueryFilter
    {
        public string Key { get; }
        public string Value { get; }
        public string Operation { get; }

        public QueryFilter(string key, string value, string operation = QueryComparisons.Equal)
        {
            Key = key?.Trim() ?? throw new ArgumentNullException(nameof(key));
            Value = value?.Trim() ?? throw new ArgumentNullException(nameof(value));
            Operation = operation.ToLowerInvariant().Trim() ?? throw new ArgumentNullException(nameof(operation));
        }

        public override string ToString()
        {
            return TableQuery.GenerateFilterCondition(Key, Operation, Value);
        }
    }
}
