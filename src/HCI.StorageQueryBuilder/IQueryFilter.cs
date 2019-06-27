using System;

namespace HCI.StorageQueryBuilder
{
	public interface IQueryFilter
	{
		string Key { get; }
		string Value { get; }
		string Operation { get; }
	}
}
