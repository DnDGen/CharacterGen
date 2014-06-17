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
        private List<String> testedNames;

        public CollectionTests()
        {
            testedNames = new List<String>();
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
                Assert.That(table.Keys, Contains.Item(name));

            var missingNames = names.Except(table.Keys);
            Assert.That(missingNames, Is.Empty, tableName);
        }

        [Test, TestFixtureTearDown]
        public void AllNamesTested()
        {
            var missingNames = table.Keys.Except(testedNames);
            Assert.That(missingNames, Is.Empty, tableName);
        }

        protected void AssertCollection(String name, IEnumerable<String> collection)
        {
            var testedBefore = testedNames.Contains(name);
            Assert.That(testedBefore, Is.False);

            testedNames.Add(name);

            Assert.That(table.Keys, Contains.Item(name), tableName);

            foreach (var item in collection)
                Assert.That(table[name], Contains.Item(item));

            var extras = table[name].Except(collection);
            Assert.That(extras, Is.Empty);
        }
    }
}