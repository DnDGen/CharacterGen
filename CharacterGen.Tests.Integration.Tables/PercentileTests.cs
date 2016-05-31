using CharacterGen.Domain.Mappers.Percentiles;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Tables
{
    [TestFixture]
    public abstract class PercentileTests : TableTests
    {
        [Inject]
        internal PercentileMapper PercentileMapper { get; set; }

        protected const string EmptyContent = "";

        private Dictionary<int, string> table;

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

        public virtual void Percentile(int lower, int upper, string content)
        {
            for (var roll = lower; roll <= upper; roll++)
                AssertPercentile(content, roll);
        }

        private void AssertPercentile(string content, int roll)
        {
            Assert.That(table.Keys, Contains.Item(roll), tableName);
            Assert.That(table[roll], Is.EqualTo(content), $"Roll: {roll}");
        }
    }
}