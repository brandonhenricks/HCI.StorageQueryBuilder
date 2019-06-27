using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
	public interface IStorageQueryBuilder
	{
		int FilterCount { get; }
		IReadOnlyList<IQueryFilter> Filters { get; }
		IStorageQueryBuilder AddFilter(IQueryFilter filter);
		IStorageQueryBuilder AddFilter(string key, string value, string operation = QueryComparisons.Equal);
		IStorageQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams);
		IStorageQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams);
		IStorageQueryBuilder Select(IList<string> columns);
		IStorageQuery Build();
	}
}
