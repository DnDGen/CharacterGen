using CharacterGen.Mappers;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : TableTests
    {
        [Inject]
        public CollectionsMapper CollectionsMapper { get; set; }

        protected Dictionary<string, IEnumerable<string>> table;
        protected Dictionary<int, string> indices;

        [SetUp]
        public void CollectionSetup()
        {
            table = CollectionsMapper.Map(tableName);
            indices = new Dictionary<int, string>();
        }

        public abstract void CollectionNames();

        protected void AssertCollectionNames(IEnumerable<string> names)
        {
            var distinctCollection = names.Distinct();
            Assert.That(distinctCollection.Count(), Is.EqualTo(names.Count()), "Provided collection is not distinct");

            AssertMissingItems(table.Keys, names);
            AssertExtraItems(table.Keys, names);
        }

        protected virtual void PopulateIndices(IEnumerable<string> collection)
        {
            for (var i = 0; i < collection.Count(); i++)
                indices[i] = string.Empty;
        }

        public virtual void Collection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            if (table[name].Count() == 1 && collection.Count() == 1)
                Assert.That(table[name].Single(), Is.EqualTo(collection.Single()));

            AssertMissingItems(table[name], collection);
            AssertExtraItems(table[name], collection);
        }

        protected void AssertMissingItems(IEnumerable<string> expected, IEnumerable<string> collection)
        {
            var missingItems = collection.Except(expected);
            Assert.That(missingItems, Is.Empty, "missing");
        }

        protected void AssertExtraItems(IEnumerable<string> expected, IEnumerable<string> collection)
        {
            var extras = expected.Except(collection);
            Assert.That(extras, Is.Empty, "extra");
            Assert.That(expected.Count(), Is.EqualTo(collection.Count()));
        }

        public virtual void OrderedCollection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            PopulateIndices(collection);

            foreach (var index in indices.Keys.OrderBy(k => k))
            {
                var actualItem = table[name].ElementAt(index);
                var expectedItem = collection[index];

                var message = string.Format("Index {0}", index);
                if (string.IsNullOrEmpty(indices[index]) == false)
                    message += string.Format(" ({0})", indices[index]);

                Assert.That(actualItem, Is.EqualTo(expectedItem), message);
            }

            AssertExtraItems(table[name], collection);
        }

        public virtual void DistinctCollection(string name, params string[] collection)
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