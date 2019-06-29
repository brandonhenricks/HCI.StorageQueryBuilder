using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
    /// <summary>
    /// <see cref="IQueryBuilder"/>: Create <see cref="TableQuery"/> with FluentApi syntax.
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
        /// Returns the current columns selected.
        /// </summary>
        IReadOnlyList<string> Columns { get; }

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
        /// Add a collection of <see cref="IQueryFilter"/> to <see cref="Filters"/>.
        /// </summary>
        /// <param name="queryParams"></param>
        IQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams);

        /// <summary>
        /// Add a collection of <see cref="IQueryFilter"/> to <see cref="Filters"/>.
        /// </summary>
        /// <param name="queryParams"></param>
        IQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams);

        /// <summary>
        /// Remove a <see cref="IQueryFilter"/> from <see cref="Filters"/>.
        /// </summary>
        /// <param name="filter"></param>
        IQueryBuilder RemoveFilter(IQueryFilter filter);

        /// <summary>
        /// Remove a <see cref="IQueryFilter"/> from <see cref="Filters"/> by <paramref name="key"/>.
        /// </summary>
        /// <param name="key"></param>
        IQueryBuilder RemoveFilter(string key);

        /// <summary>
        /// Remove <paramref name="filters"/> from <see cref="Filters"/>.
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        IQueryBuilder RemoveFilters(IEnumerable<IQueryFilter> filters);

        /// <summary>
        /// Remove all <see cref="IQueryFilter"/> from <see cref="Filters"/>.
        /// </summary>
        /// <returns></returns>
        IQueryBuilder RemoveFilters();

        /// <summary>
        /// Adds a range of columns to <see cref="Columns"/> for returning.
        /// </summary>
        /// <param name="columns"></param>
        IQueryBuilder Select(IList<string> columns);

        /// <summary>
        /// Build the <see cref="IStorageQuery"/> to return a <see cref="IStorageQuery"/>.
        /// </summary>
        IStorageQuery Build();

        /// <summary>
        /// Removes all <see cref="IQueryFilter"/> and <see cref="Columns"/>.
        /// </summary>
        void Clear();
    }
}
