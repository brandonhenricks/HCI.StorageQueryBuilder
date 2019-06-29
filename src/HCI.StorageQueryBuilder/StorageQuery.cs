using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
    internal sealed class StorageQuery : IStorageQuery
    {
        private readonly IList<string> _columns;
        private readonly IList<IQueryFilter> _queryFilters;

        public StorageQuery(IList<string> columns, IList<IQueryFilter> queryFilters)
        {
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
            _queryFilters = queryFilters ?? throw new ArgumentNullException(nameof(queryFilters));
        }

        public StorageQuery()
        {
            _queryFilters = new List<IQueryFilter>();
            _columns = new List<string>();
        }

        public TableQuery ToTableQuery()
        {
            var query = BuildQueryWhereClause(_queryFilters);

            if (HasColumns(_columns))
            {
                query.Select(_columns);
            }

            _queryFilters.Clear();
            _columns.Clear();

            return query;
        }

        public TableQuery<T> ToTableQuery<T>() where T : ITableEntity, new()
        {
            var query = new TableQuery<T>();

            foreach (var item in _queryFilters)
            {
                query.Where(TableQuery.GenerateFilterCondition(item.Key, item.Operation, item.Value));
            }

            if (HasColumns(_columns))
            {
                query.Select(_columns);
            }

            _queryFilters.Clear();
            _columns.Clear();

            return query;
        }

        private static bool HasColumns(IList<string> columns) => columns.Count > 0;

        private static TableQuery BuildQueryWhereClause(IEnumerable<IQueryFilter> queryFilters)
        {
            var query = new TableQuery();

            foreach (var item in queryFilters)
            {
                query.Where(TableQuery.GenerateFilterCondition(item.Key, item.Operation, item.Value));
            }

            return query;
        }

    }
}
