using System;

namespace HCI.StorageQueryBuilder
{
	/// <summary>
	/// Creates a Filter Condition for <see cref="IStorageQuery"/>.
	/// </summary>
	public interface IQueryFilter
	{
		/// <summary>
		/// Query Filter Key
		/// </summary>
		/// <example>PartitionKey</example>
		string Key { get; }

		/// <summary>
		/// Query Filter Value
		/// </summary>
		/// <example>Test</example>
		string Value { get; }

		/// <summary>
		/// Query Filter Operation
		/// </summary>
		/// <example>eq</example>
		string Operation { get; }
	}
}
