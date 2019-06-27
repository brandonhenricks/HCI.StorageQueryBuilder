using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
	/// <summary>
	/// QueryBuilder - Create TableQuery with FluentApi
	/// </summary>
	public interface IQueryBuilder
	{
		/// <summary>
		/// Returns the current Column Count.
		/// </summary>
		int ColumnCount { get; }

		/// <summary>
		/// Returns the current <see cref="IQueryFilter"/> count.
		/// </summary>
		int FilterCount { get; }

		/// <summary>
		/// Returns the current <see cref="IQueryFilter"/> as <see cref="IReadOnlyCollection{IQueryFilter}"/>
		/// </summary>
		IReadOnlyList<IQueryFilter> Filters { get; }

		/// <summary>
		/// Add a <see cref="IQueryFilter"/>.
		/// </summary>
		/// <param name="filter"></param>
		IQueryBuilder AddFilter(IQueryFilter filter);

		/// <summary>
		/// Add a <see cref="IQueryFilter"/>
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		/// <param name="operation"></param>
		IQueryBuilder AddFilter(string key, string value, string operation = QueryComparisons.Equal);

		/// <summary>
		/// Add a collection of <see cref="IQueryFilter"/>.
		/// </summary>
		/// <param name="queryParams"></param>
		IQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams);

		/// <summary>
		/// Add a collection of <see cref="IQueryFilter"/>.
		/// </summary>
		/// <param name="queryParams"></param>
		IQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams);

		/// <summary>
		/// Add a list of columns to return.
		/// </summary>
		/// <param name="columns"></param>
		IQueryBuilder Select(IList<string> columns);

		/// <summary>
		/// Build the <see cref="IStorageQuery"/> to return a Query.
		/// </summary>
		IStorageQuery Build();

		/// <summary>
		/// Removes all <see cref="IQueryFilter"/> and Column Names.
		/// </summary>
		void Clear();
	}
}
