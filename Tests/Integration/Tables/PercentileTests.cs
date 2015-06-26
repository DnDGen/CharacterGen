using System;
using System.Collections.Generic;
using Ninject;
using NPCGen.Mappers.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class PercentileTests : TableTests
    {
        [Inject]
        public IPercentileMapper PercentileMapper { get; set; }

        protected const String EmptyContent = "";

        private Dictionary<Int32, String> table;

        [SetUp]
        public void PercentileSetup()
        {
            table = PercentileMapper.Map(tableName);
        }

        public abstract void TableIsComplete();

        protected void AssertTableIsComplete()
        {
            for (var roll = 100; roll > 0; roll--)
                Assert.That(table.Keys, Contains.Item(roll), tableName);

            Assert.That(table.Keys.Count, Is.EqualTo(100), tableName);
        }

        public virtual void Percentile(String content, Int32 roll)
        {
            AssertPercentile(content, roll);
        }

        public virtual void Percentile(String content, Int32 lower, Int32 upper)
        {
            for (var roll = lower; roll <= upper; roll++)
                AssertPercentile(content, roll);
        }

        private void AssertPercentile(String content, Int32 roll)
        {
            Assert.That(table.Keys, Contains.Item(roll), tableName);

            var message = String.Format("Roll: {0}", roll);
            Assert.That(table[roll], Is.EqualTo(content), message);
        }
    }
}