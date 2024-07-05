using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Mappers.Collections;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Abilities.Races
{
    [TestFixture]
    public class CrossRaceAbilityAdjustmentsTests : TableTests
    {
        protected override string tableName
        {
            get
            {
                return TableNameConstants.Set.Collection.BaseRaceGroups;
            }
        }

        private CollectionMapper collectionsMapper;

        [SetUp]
        public void Setup()
        {
            collectionsMapper = GetNewInstanceOf<CollectionMapper>();
        }

        [Test]
        public void AllBaseRacesHaveAbilityAdjustmentTables()
        {
            var baseRaceGroups = collectionsMapper.Map(Config.Name, TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, baseRace);
                var abilityAdjustments = collectionsMapper.Map(Config.Name, tableName);

                Assert.That(abilityAdjustments, Is.Not.Null);
                Assert.That(abilityAdjustments, Is.Not.Empty);
            }
        }

        [Test]
        public void AllMetaracesHaveAbilityAdjustmentTables()
        {
            var metaraceGroups = collectionsMapper.Map(Config.Name, TableNameConstants.Set.Collection.MetaraceGroups);
            var allMetaraces = metaraceGroups[GroupConstants.All];

            foreach (var metarace in allMetaraces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAbilityAdjustments, metarace);
                var abilityAdjustments = collectionsMapper.Map(Config.Name, tableName);

                Assert.That(abilityAdjustments, Is.Not.Null);
                Assert.That(abilityAdjustments, Is.Not.Empty);
            }
        }
    }
}
