using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Mappers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : TableTests
    {
        [Inject]
        public ICollectionsMapper CollectionsMapper { get; set; }

        protected Dictionary<String, IEnumerable<String>> table;
        protected Dictionary<Int32, String> indices;

        [SetUp]
        public void CollectionSetup()
        {
            table = CollectionsMapper.Map(tableName);
            indices = new Dictionary<Int32, String>();
        }

        protected virtual void PopulateIndices(IEnumerable<String> collection)
        {
            for (var i = 0; i < collection.Count(); i++)
                indices[i] = String.Format("Index {0}", i);
        }

        public virtual void Collection(String name, params String[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            foreach (var item in collection)
                Assert.That(table[name], Contains.Item(item), tableName);

            AssertExtraItems(name, collection);
        }

        protected void AssertExtraItems(String name, IEnumerable<String> collection)
        {
            var extras = table[name].Except(collection);
            Assert.That(extras, Is.Empty, name);
        }

        public virtual void OrderedCollection(String name, params String[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            PopulateIndices(collection);

            for (var i = 0; i < collection.Length; i++)
            {
                var actualItem = table[name].ElementAt(i);
                var expectedItem = collection[i];

                Assert.That(actualItem, Is.EqualTo(expectedItem));
            }

            AssertExtraItems(name, collection);
        }

        public virtual void DistinctCollection(String name, params String[] collection)
        {
            var distinctCollection = collection.Distinct();
            Assert.That(distinctCollection.Count(), Is.EqualTo(collection.Count()));

            Collection(name, collection);

            distinctCollection = table[name].Distinct();
            Assert.That(distinctCollection.Count(), Is.EqualTo(table[name].Count()));
        }

        public virtual void EmptyCollection()
        {
            Assert.That(table, Is.Empty, tableName);
        }
    }
}