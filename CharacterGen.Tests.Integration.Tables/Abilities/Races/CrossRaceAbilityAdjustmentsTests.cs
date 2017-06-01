using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Tables;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class CrossRaceAbilityAdjustmentsTests : IntegrationTests
    {
        [Inject]
        internal CollectionsMapper CollectionsMapper { get; set; }

        [Test]
        public void AllBaseRacesHaveAbilityAdjustmentTables()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, baseRace);
                var abilityAdjustments = CollectionsMapper.Map(tableName);

                Assert.That(abilityAdjustments, Is.Not.Null);
                Assert.That(abilityAdjustments, Is.Not.Empty);
            }
        }

        [Test]
        public void AllMetaracesHaveAbilityAdjustmentTables()
        {
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var allMetaraces = metaraceGroups[GroupConstants.All];

            foreach (var metarace in allMetaraces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, metarace);
                var abilityAdjustments = CollectionsMapper.Map(tableName);

                Assert.That(abilityAdjustments, Is.Not.Null);
                Assert.That(abilityAdjustments, Is.Not.Empty);
            }
        }
    }
}
