using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HCI.StorageQueryBuilder.Tests
{
	[TestClass]
	public class QueryBuilderTests
	{
		[TestMethod]
		public void QueryBuilder_Constructor_Null_Argument_ThrowsException()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new QueryBuilder(null));
		}
		[TestMethod]
		public void QueryBuilder_Constructor_Null_Arguments_ThrowsException()
		{
			Assert.ThrowsException<ArgumentNullException>(() => new QueryBuilder(null, null));
		}
	}
}
