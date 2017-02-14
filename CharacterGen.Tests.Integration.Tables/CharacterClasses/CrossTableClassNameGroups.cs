using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Mappers.Percentiles;
using CharacterGen.Domain.Tables;
using CharacterGen.Magics;
using Ninject;
using NUnit.Framework;
using System.Linq;

namespace CharacterGen.Tests.Integration.Tables.CharacterClasses
{
    [TestFixture]
    public class CrossTableClassNameGroups : IntegrationTests
    {
        [Inject]
        internal CollectionsMapper CollectionsMapper { get; set; }
        [Inject]
        internal PercentileMapper PercentileMapper { get; set; }

        [Test]
        public void AllClassesHaveHasSpecialistFieldTable()
        {
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            foreach (var className in classGroups[GroupConstants.All])
            {
                var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSHasSpecialistFields, className);
                var table = PercentileMapper.Map(tableName);
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
            var classGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.ClassNameGroups);

            foreach (var className in classGroups[SpellConstants.Sources.Arcane])
            {
                var tableName = string.Format(TableNameConstants.Formattable.TrueOrFalse.CLASSKnowsAdditionalSpells, className);
                var table = PercentileMapper.Map(tableName);
                Assert.That(table, Is.Not.Null);
                Assert.That(table, Is.Not.Empty);

                var indices = Enumerable.Range(1, 100);
                Assert.That(table.Keys, Is.EquivalentTo(indices));
                Assert.That(table.Values, Is.All.EqualTo(bool.TrueString).Or.EqualTo(bool.FalseString));
            }
        }
    }
}
