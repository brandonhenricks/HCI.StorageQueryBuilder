using System;
using System.Collections.Generic;
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

        public override bool Equals(object obj)
        {
            return obj is QueryFilter filter &&
                   Key == filter.Key &&
                   Value == filter.Value;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 206514262;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Key);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
                return hashCode;
            }
        }

        public static bool operator ==(QueryFilter left, QueryFilter right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(QueryFilter left, QueryFilter right)
        {
            return !(left == right);
        }
    }
}
