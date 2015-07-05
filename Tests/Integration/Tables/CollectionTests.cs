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
                indices[i] = String.Empty;
        }

        public virtual void Collection(String name, params String[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            var missingItems = collection.Except(table[name]);
            Assert.That(missingItems, Is.Empty, name);

            AssertExtraItems(name, collection);
        }

        protected void AssertExtraItems(String name, IEnumerable<String> collection)
        {
            var extras = table[name].Except(collection);
            Assert.That(extras, Is.Empty, name);
            Assert.That(table[name].Count(), Is.EqualTo(collection.Count()));
        }

        public virtual void OrderedCollection(String name, params String[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            PopulateIndices(collection);

            foreach (var index in indices.Keys.OrderBy(k => k))
            {
                var actualItem = table[name].ElementAt(index);
                var expectedItem = collection[index];

                var message = String.Format("Index {0}", index);
                if (String.IsNullOrEmpty(indices[index]) == false)
                    message += String.Format(" ({0})", indices[index]);

                Assert.That(actualItem, Is.EqualTo(expectedItem), message);
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