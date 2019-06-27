using System;
using System.Linq;
using System.Collections.Generic;

namespace HCI.StorageQueryBuilder
{
	public sealed class StorageQueryBuilder : IStorageQueryBuilder
	{
		private readonly IList<IQueryFilter> _queryFilters;
		private readonly IList<string> _columns;

		public int FilterCount => _queryFilters.Count;

		public IReadOnlyList<IQueryFilter> Filters => _queryFilters.ToList();

		public StorageQueryBuilder()
		{
			_queryFilters = new List<IQueryFilter>();
			_columns = new List<string>();
		}

		public StorageQueryBuilder(IList<IQueryFilter> queryFilters)
		{
			_queryFilters = queryFilters ?? throw new ArgumentNullException(nameof(queryFilters));
		}

		public StorageQueryBuilder(IList<IQueryFilter> queryFilters, IList<string> columns) : this(queryFilters)
		{
			_queryFilters = queryFilters ?? throw new ArgumentNullException(nameof(queryFilters));
			_columns = columns ?? throw new ArgumentNullException(nameof(columns));
		}

		public IStorageQueryBuilder AddFilter(IQueryFilter filter)
		{
			_queryFilters.Add(filter);
			return this;
		}

		public IStorageQueryBuilder AddFilter(string key, string value, string operation = "eq")
		{
			_queryFilters.Add(new QueryFilter(key, value, operation));
			return this;
		}

		public IStorageQueryBuilder AddFilters(IEnumerable<KeyValuePair<string, string>> queryParams)
		{
			foreach (var query in queryParams)
			{
				AddFilter(query.Key, query.Value);
			}

			return this;
		}

		public IStorageQueryBuilder AddFilters(IEnumerable<IQueryFilter> queryParams)
		{
			foreach (var query in queryParams)
			{
				_queryFilters.Add(query);
			}

			return this;
		}

		public IStorageQueryBuilder Select(IList<string> columns)
		{
			foreach (var column in columns)
			{
				_columns.Add(column?.Trim());
			}

			return this;
		}

		public IStorageQuery Build()
		{
			return new StorageQuery();
		}
	}
}
