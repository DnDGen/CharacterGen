using DnDGen.CharacterGen.Magics;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Mappers.Collections;
using DnDGen.Infrastructure.Mappers.Percentiles;
using NUnit.Framework;
using System.Linq;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class CrossTableClassNameGroups : TableTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.ClassNameGroups;
            }
        }

        private CollectionMapper collectionsMapper;
        private PercentileMapper percentileMapper;

        [SetUp]
        public void Setup()
        {
            collectionsMapper = GetNewInstanceOf<CollectionMapper>();
            percentileMapper = GetNewInstanceOf<PercentileMapper>();
        }

        [Test]
        public void AllClassesHaveHasSpecialistFieldTable()
        {
            var classGroups = collectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            foreach (var className in classGroups[GroupConstants.All])
            {
                var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, className);
                var table = percentileMapper.Map(tableName);
                Assert.That(table, Is.Not.Null);
                Assert.That(table, Is.Not.Empty);

                var indices = Enumerable.Range(1, 100);
                Assert.That(table.Keys, Is.EquivalentTo(indices));
                Assert.That(table.Values, Is.All.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString));
            }
        }

        [Test]
        public void AllArcaneSpellcasterClassesHaveKnowsAdditionalSpellsTable()
        {
            var classGroups = collectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            foreach (var className in classGroups[SpellConstants.Sources.Arcane])
            {
                var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, className);
                var table = percentileMapper.Map(tableName);
                Assert.That(table, Is.Not.Null);
                Assert.That(table, Is.Not.Empty);

                var indices = Enumerable.Range(1, 100);
                Assert.That(table.Keys, Is.EquivalentTo(indices));
                Assert.That(table.Values, Is.All.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString));
            }
        }
    }
}
