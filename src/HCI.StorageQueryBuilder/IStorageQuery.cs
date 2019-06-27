using System;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder
{
    public interface IStorageQuery
    {
        /// <summary>
        /// Generate a <see cref="TableQuery"/> from <see cref="IQueryBuilder"/>.
        /// </summary>
        /// <returns></returns>
        TableQuery ToTableQuery();

        /// <summary>
        /// Generate a <see cref="TableQuery{T}"/> from <see cref="IQueryBuilder"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        TableQuery<T> ToTableQuery<T>() where T : ITableEntity, new();
    }
}
