using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
	public interface IStorageQuery
	{
		TableQuery ToTableQuery();
		TableQuery<T> ToTableQuery<T>() where T : ITableEntity, new();
	}
}
