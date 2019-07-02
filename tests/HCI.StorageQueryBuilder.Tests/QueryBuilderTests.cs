using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.WindowsAzure.Storage.Table;

namespace HCI.StorageQueryBuilder.Tests
{
    [TestClass]
    public class QueryBuilderTests
    {
        [TestMethod]
        public void QueryBuilder_Null_Constructor_Returns_Correct_Counts()
        {
            var builder = new QueryBuilder();
            Assert.AreEqual(0, builder.FilterCount);
            Assert.AreEqual(0, builder.ColumnCount);
        }

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

        [TestMethod]
        public void QueryBuilder_AddFilter_Null_Key_ThrowsException()
        {
            var builder = new QueryBuilder();

            Assert.ThrowsException<ArgumentNullException>(() => builder.AddFilter(null, "test"));
        }

        [TestMethod]
        public void QueryBuilder_AddFilter_Null_Value_ThrowsException()
        {
            var builder = new QueryBuilder();

            Assert.ThrowsException<ArgumentNullException>(() => builder.AddFilter("test", null));
        }

        [TestMethod]
        public void QueryBuilder_AddFilter_Null_Arguments_ThrowsException()
        {
            var builder = new QueryBuilder();

            Assert.ThrowsException<ArgumentNullException>(() => builder.AddFilter(null, null));
        }

        [TestMethod]
        public void QueryBuilder_AddFilter_Returns_Correct_Count()
        {
            var builder = new QueryBuilder()
                .AddFilter("PartitionKey", "partition");

            Assert.AreEqual(1, builder.FilterCount);
        }

        [TestMethod]
        public void QueryBuilder_Select_Returns_Correct_Count()
        {
            var builder = new QueryBuilder()
                .Select(new List<string>() { "PartitionKey" });

            Assert.AreEqual(1, builder.ColumnCount);
        }

        [TestMethod]
        public void QueryBuilder_Clear_Returns_Correct_Count()
        {
            var builder = new QueryBuilder()
                .AddFilter("PartitionKey", "test")
                .AddFilter("RowKey", "000-000-000")
                .Select(new List<string>() { "PartitionKey" });

            Assert.AreEqual(1, builder.ColumnCount);

            builder.Clear();

            Assert.AreEqual(0, builder.ColumnCount);
            Assert.AreEqual(0, builder.FilterCount);
        }

        [TestMethod]
        public void QueryBuilder_Builder_Returns_IStorageQuery()
        {
            var builder = new QueryBuilder()
                .Select(new List<string>() { "PartitionKey" })
                .Build();

            Assert.IsInstanceOfType(builder, typeof(IStorageQuery));
        }

        [TestMethod]
        public void QueryBuilder_ToTableQuery_Returns_TableQuery()
        {
            var builder = new QueryBuilder()
                .Select(new List<string>() { "PartitionKey" })
                .Build()
                .ToTableQuery();

            Assert.IsInstanceOfType(builder, typeof(TableQuery));
        }

        [TestMethod]
        public void QueryBuilder_ToTableQueryT_Returns_TableQueryT()
        {
            var builder = new QueryBuilder()
                .Select(new List<string>() { "PartitionKey" })
                .Build()
                .ToTableQuery<TableEntity>();

            Assert.IsInstanceOfType(builder, typeof(TableQuery<TableEntity>));
        }

        [TestMethod]
        public void QueryBuilder_RemoveFilters_Returns_Correct_Filter_Count()
        {
            var builder = new QueryBuilder()
                .AddFilter("PartitionKey", "test")
                .AddFilter("RowKey", "000-000-000")
                .Select(new List<string>() { "PartitionKey" });

            Assert.AreEqual(1, builder.ColumnCount);

            builder.RemoveFilters();

            Assert.AreEqual(1, builder.ColumnCount);
            Assert.AreEqual(0, builder.FilterCount);
        }

        [TestMethod]
        public void QueryBuilder_RemoveFilter_Filter_Not_Found_Throws_Exception()
        {
            var builder = new QueryBuilder()
                .AddFilter("PartitionKey", "test")
                .AddFilter("RowKey", "000-000-000")
                .Select(new List<string>() { "PartitionKey" });

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => builder.RemoveFilter("test"));
        }

        [TestMethod]
        public void QueryBuilder_RemoveFilter_FilterCount_Correct()
        {
            var builder = new QueryBuilder()
                .AddFilter("PartitionKey", "test")
                .AddFilter("RowKey", "000-000-000")
                .Select(new List<string>() { "PartitionKey" });

            builder.RemoveFilter("RowKey");

            Assert.AreEqual(1, builder.FilterCount);
        }

        [TestMethod]
        public void QueryBuilder_AddFilters_FilterCount_Correct()
        {
            var filters = new List<IQueryFilter>(2)
            {
                new QueryFilter("RowKey", Guid.NewGuid().ToString()),
                new QueryFilter("PartitionKey", "test")
            };

            var builder = new QueryBuilder()
                .AddFilters(filters);

            Assert.AreEqual(2, builder.FilterCount);
        }

        [TestMethod]
        public void QueryBuilder_RemoveFilters_FilterCount_Correct()
        {
            var filters = new List<IQueryFilter>(2)
            {
                new QueryFilter("RowKey", Guid.NewGuid().ToString()),
                new QueryFilter("PartitionKey", "test")
            };

            var builder = new QueryBuilder()
                .AddFilters(filters)
                .AddFilter("Test", "Test");

            builder.RemoveFilters(filters);

            Assert.AreEqual(1, builder.FilterCount);
        }

        [TestMethod]
        public void QueryBuilder_Select_With_Duplicates_IgnoresDupes_Correct_Count()
        {
            var columns = new List<string>(5)
            {
                "test",
                "test1",
                "RowKey",
                "PartitionKey",
                "RowKey"
            };

            var builder = new QueryBuilder()
                .Select(columns);

            Assert.AreNotEqual(columns.Count, builder.ColumnCount);

            Assert.AreEqual(columns.Count - 1, builder.ColumnCount);
        }
    }
}
