using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class DataTests : CollectionTests
    {
        protected Dictionary<Int32, String> dataIndices;

        [SetUp]
        public void DataTestsSetup()
        {
            dataIndices = new Dictionary<Int32, String>();
        }

        protected abstract Dictionary<Int32, String> PopulateDataIndices();

        protected override void PopulateIndices(IEnumerable<String> collection)
        {
            indices = dataIndices;
        }

        protected void Data(String name, params String[] data)
        {
            OrderedCollection(name, data);
        }
    }
}