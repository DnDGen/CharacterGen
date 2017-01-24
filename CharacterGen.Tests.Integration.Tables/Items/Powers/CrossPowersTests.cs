using CharacterGen.Domain.Mappers.Percentiles;
using CharacterGen.Domain.Tables;
using Ninject;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.Items.Powers
{
    [TestFixture]
    public class CrossPowersTests : IntegrationTests
    {
        [Inject]
        internal PercentileMapper PercentileMapper { get; set; }

        private IEnumerable<int> levels;
        private IEnumerable<int> indices;

        [SetUp]
        public void Setup()
        {
            levels = Enumerable.Range(1, 30);
            indices = Enumerable.Range(1, 100);
        }

        [Test]
        public void PowerTableExistsForAllLevels()
        {
            foreach (var level in levels)
            {
                AssertTable(level);
            }
        }

        private void AssertTable(int level)
        {
            var tableName = string.Format(TableNameConstants.Formattable.Percentile.LevelXPower, level);
            var table = PercentileMapper.Map(tableName);

            Assert.That(table, Is.Not.Null);
            Assert.That(table.Keys, Is.EquivalentTo(indices));
            Assert.That(table.Values, Is.All.Not.Null);
            Assert.That(table.Values, Is.All.Not.Empty);
        }
    }
}
