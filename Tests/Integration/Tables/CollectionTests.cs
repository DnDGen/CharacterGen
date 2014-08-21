using System;
using System.Collections.Generic;
using System.Linq;
using Ninject;
using NPCGen.Mappers.Interfaces;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : IntegrationTests
    {
        [Inject]
        public ICollectionsMapper CollectionsMapper { get; set; }

        protected abstract String tableName { get; }
        protected abstract IEnumerable<String> nameCollection { get; }

        private Dictionary<String, IEnumerable<String>> table;
        private HashSet<String> testedNames;

        public CollectionTests()
        {
            testedNames = new HashSet<String>();
        }

        [SetUp]
        public void CollectionSetup()
        {
            table = CollectionsMapper.Map(tableName);
        }

        [Test]
        public void AllNamesInCollection()
        {
            var names = nameCollection;

            foreach (var name in names)
                Assert.That(table.Keys, Contains.Item(name), tableName);

            var missingNames = names.Except(table.Keys);
            Assert.That(missingNames, Is.Empty, tableName);
        }

        [Test, TestFixtureTearDown]
        public void AllNamesTested()
        {
            var missingNames = table.Keys.Except(testedNames);
            Assert.That(missingNames, Is.Empty, tableName);
        }

        public virtual void Collection(String name, params String[] collection)
        {
            AssertNewNameToTest(name);

            foreach (var item in collection)
                Assert.That(table[name], Contains.Item(item));

            AssertExtraItems(name, collection);
        }

        private void AssertNewNameToTest(String name)
        {
            var newNameToTest = testedNames.Add(name);
            Assert.That(newNameToTest, Is.True);
            Assert.That(table.Keys, Contains.Item(name), tableName);
        }

        private void AssertExtraItems(String name, IEnumerable<String> collection)
        {
            var extras = table[name].Except(collection);
            Assert.That(extras, Is.Empty);
        }

        public virtual void OrderedCollection(String name, params String[] collection)
        {
            AssertNewNameToTest(name);

            for (var i = 0; i < collection.Length; i++)
            {
                var actualItem = table[name].ElementAt(i);
                var expectedItem = collection[i];

                Assert.That(actualItem, Is.EqualTo(expectedItem));
            }

            AssertExtraItems(name, collection);
        }
    }
}