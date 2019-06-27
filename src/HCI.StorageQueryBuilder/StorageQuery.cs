using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
	public class StorageQuery : IStorageQuery
	{
		public StorageQuery()
		{
		}

		public TableQuery ToTableQuery()
		{
			throw new NotImplementedException();
		}

		public TableQuery<T> ToTableQuery<T>() where T : ITableEntity, new()
		{
			throw new NotImplementedException();
		}
	}
}
