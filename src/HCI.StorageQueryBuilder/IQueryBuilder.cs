using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
	/// <summary>
	/// QueryBuilder - Create TableQuery with FluentApi
	/// </summary>
	public interface IQueryBuilder
	{
		int FilterCount { get; }
		IReadOnlyList<IQueryFilter> Filters { get; }
		IQueryBuilder AddFilter(IQueryFilter filter);
		IQueryBuilder AddFilter(string key, string value, string operation = QueryComparisons.Equal);
		IQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams);
		IQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams);
		IQueryBuilder Select(IList<string> columns);
		IStorageQuery Build();
	}
}
