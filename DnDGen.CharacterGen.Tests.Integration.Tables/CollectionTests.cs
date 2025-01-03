﻿using DnDGen.Infrastructure.Mappers.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class CollectionTests : TableTests
    {
        protected CollectionMapper collectionsMapper;

        protected Dictionary<string, IEnumerable<string>> table;
        protected Dictionary<int, string> indices;

        [SetUp]
        public void CollectionSetup()
        {
            collectionsMapper = GetNewInstanceOf<CollectionMapper>();

            table = GetTable(tableName);
            indices = [];
        }

        public abstract void CollectionNames();

        protected Dictionary<string, IEnumerable<string>> GetTable(string table)
        {
            return collectionsMapper.Map(Config.Name, table);
        }

        protected void AssertCollectionNames(IEnumerable<string> names)
        {
            AssertUnique(names);
            AssertCollection(table.Keys, names);
        }

        protected IEnumerable<string> GetCollection(string name)
        {
            return table[name];
        }

        private void AssertUnique(IEnumerable<string> collection)
        {
            var duplicateItems = collection.Where(s => collection.Count(c => c == s) > 1);
            var duplicates = string.Join(", ", duplicateItems.Distinct());

            Assert.That(collection, Is.Unique, $"Collection is not distinct: {duplicates}");
        }

        protected virtual void PopulateIndices(IEnumerable<string> collection)
        {
            for (var i = 0; i < collection.Count(); i++)
                indices[i] = string.Empty;
        }

        public virtual void Collection(string name, params string[] collection)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);
            AssertCollection(table[name], collection);
        }

        protected void AssertCollection(IEnumerable<string> actual, IEnumerable<string> expected)
        {
            Assert.That(actual, Is.EquivalentTo(expected));
        }

        public virtual void OrderedCollection(string name, params string[] expected)
        {
            Assert.That(table.Keys, Contains.Item(name), tableName);

            PopulateIndices(expected);
            var actual = table[name].ToArray();

            foreach (var index in indices.Keys.OrderBy(k => k))
            {
                var message = $"Index {index}";
                if (!string.IsNullOrEmpty(indices[index]))
                    message += $" ({indices[index]})";

                Assert.That(actual[index], Is.EqualTo(expected[index]), message);
            }

            Assert.That(table[name], Is.EqualTo(expected));
        }

        public virtual void DistinctCollection(string name, params string[] expected)
        {
            AssertUnique(expected);
            Collection(name, expected);
            AssertUnique(table[name]);
        }
    }
}