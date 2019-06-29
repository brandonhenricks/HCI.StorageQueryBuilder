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
            var query = new TableQuery();

            foreach (var item in _queryFilters)
            {
                query.Where(TableQuery.GenerateFilterCondition(item.Key, item.Operation, item.Value));
            }

            if (_columns.Count > 0)
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

            if (_columns.Count > 0)
            {
                query.Select(_columns);
            }

            _queryFilters.Clear();
            _columns.Clear();

            return query;
        }
    }
}
