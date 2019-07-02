using System;
using System.Linq;
using System.Collections.Generic;

namespace HCI.StorageQueryBuilder
{
    public sealed class QueryBuilder : IQueryBuilder
    {
        private readonly IList<IQueryFilter> _queryFilters;
        private readonly IList<string> _columns;

        public int ColumnCount => _columns.Count;

        public int FilterCount => _queryFilters.Count;

        public IReadOnlyList<IQueryFilter> Filters => _queryFilters.ToList();

        public IReadOnlyList<string> Columns => _columns.ToList();

        public QueryBuilder()
        {
            _queryFilters = new List<IQueryFilter>();
            _columns = new List<string>();
        }

        public QueryBuilder(IList<IQueryFilter> queryFilters)
        {
            _queryFilters = queryFilters ?? throw new ArgumentNullException(nameof(queryFilters));
        }

        public QueryBuilder(IList<IQueryFilter> queryFilters, IList<string> columns) : this(queryFilters)
        {
            _queryFilters = queryFilters ?? throw new ArgumentNullException(nameof(queryFilters));
            _columns = columns ?? throw new ArgumentNullException(nameof(columns));
        }

        public IQueryBuilder AddFilter(IQueryFilter filter)
        {
            if (!_queryFilters.Contains<IQueryFilter>(filter))
            {
                _queryFilters.Add(filter);
            }

            return this;
        }

        public IQueryBuilder AddFilter(string key, string value, string operation = "eq")
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            return AddFilter(new QueryFilter(key, value, operation));
        }

        public IQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams)
        {
            foreach (var query in queryParams)
            {
                var filter = new QueryFilter(query.Key, query.Value);

                if (!_queryFilters.Contains<IQueryFilter>(filter))
                {
                    _queryFilters.Add(filter);
                }
            }

            return this;
        }

        public IQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams)
        {
            foreach (var query in queryParams)
            {
                if (!_queryFilters.Contains<IQueryFilter>(query))
                {
                    _queryFilters.Add(query);
                }
            }

            return this;
        }

        public IStorageQuery Build()
        {
            return new StorageQuery(_columns, _queryFilters);
        }

        public void Clear()
        {
            _queryFilters?.Clear();
            _columns?.Clear();
        }

        public IQueryBuilder RemoveFilter(IQueryFilter filter)
        {
            if (_queryFilters.Contains(filter))
            {
                _queryFilters.Remove(filter);
            }

            return this;
        }

        public IQueryBuilder RemoveFilter(string key)
        {
            var filter = _queryFilters.FirstOrDefault(prop => prop.Key.Equals(key, StringComparison.OrdinalIgnoreCase));

            if (filter is null)
            {
                throw new ArgumentOutOfRangeException(nameof(key));
            }

            _queryFilters.Remove(filter);

            return this;
        }

        public IQueryBuilder RemoveFilters()
        {
            _queryFilters.Clear();

            return this;
        }

        public IQueryBuilder RemoveFilters(IEnumerable<IQueryFilter> filters)
        {
            var removeFilters = filters.ToList();

            foreach (var item in removeFilters)
            {
                if (_queryFilters.Contains(item))
                {
                    _queryFilters.Remove(item);
                }
            }

            removeFilters?.Clear();

            return this;
        }

        public IQueryBuilder Select(IList<string> columns)
        {
            foreach (var column in columns)
            {
                if (string.IsNullOrEmpty(column)) continue;

                if (_columns.Contains(column)) continue;

                _columns.Add(column.Trim());
            }

            return this;
        }
    }
}
