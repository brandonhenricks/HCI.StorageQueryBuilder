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
			if (filter is null) throw new ArgumentNullException(nameof(filter));

			_queryFilters.Add(filter);

			return this;
		}

		public IQueryBuilder AddFilter(string key, string value, string operation = "eq")
		{
			_queryFilters.Add(new QueryFilter(key, value, operation));

			return this;
		}

		public IQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams)
		{
			foreach (var query in queryParams)
			{
				AddFilter(query.Key, query.Value);
			}

			return this;
		}

		public IQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams)
		{
			foreach (var query in queryParams)
			{
				_queryFilters.Add(query);
			}

			return this;
		}

		public IQueryBuilder Select(IList<string> columns)
		{
			foreach (var column in columns)
			{
				_columns.Add(column?.Trim());
			}

			return this;
		}

		public IStorageQuery Build()
		{
			return new StorageQuery(_columns, _queryFilters);
		}

		public void Clear()
		{
			_queryFilters.Clear();
			_columns.Clear();
		}
	}
}
