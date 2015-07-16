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

        public abstract void CollectionNames();

        protected void AssertCollectionNames(IEnumerable<String> names)
        {
            var distinctCollection = names.Distinct();
            Assert.That(distinctCollection.Count(), Is.EqualTo(names.Count()), "Provided collection is not distinct");

            AssertMissingItems(table.Keys, names);
            AssertExtraItems(table.Keys, names);
        }

        protected virtual void PopulateIndices(IEnumerable<String> collection)
        {
            for (var i = 0; i < collection.Count(); i++)
                indices[i] = String.Empty;
        }

        public virtual void Collection(String name, params String[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            AssertMissingItems(table[name], collection);
            AssertExtraItems(table[name], collection);
        }

        protected void AssertMissingItems(IEnumerable<String> expected, IEnumerable<String> collection)
        {
            var missingItems = collection.Except(expected);
            Assert.That(missingItems, Is.Empty, "missing");
        }

        protected void AssertExtraItems(IEnumerable<String> expected, IEnumerable<String> collection)
        {
            var extras = expected.Except(collection);
            Assert.That(extras, Is.Empty, "extra");
            Assert.That(expected.Count(), Is.EqualTo(collection.Count()));
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

            AssertExtraItems(table[name], collection);
        }

        public virtual void DistinctCollection(String name, params String[] collection)
        {
            var distinctCollection = collection.Distinct();
            Assert.That(distinctCollection.Count(), Is.EqualTo(collection.Count()), "Provided collection is not distinct");

            Collection(name, collection);

            distinctCollection = table[name].Distinct();
            Assert.That(distinctCollection.Count(), Is.EqualTo(table[name].Count()), "Actual collection is not distinct");
        }

        public virtual void EmptyCollection()
        {
            Assert.That(table, Is.Empty, tableName);
        }
    }
}