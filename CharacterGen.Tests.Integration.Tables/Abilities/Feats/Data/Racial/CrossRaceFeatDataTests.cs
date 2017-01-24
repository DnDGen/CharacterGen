using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Abilities.Feats.Data.Racial
{
    [TestFixture]
    public class CrossRaceFeatDataTests : IntegrationTests
    {
        [Inject]
        internal CollectionsMapper CollectionsMapper { get; set; }

        [Test]
        public void AllBaseRacesHaveFeatDataTables()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, baseRace);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllMetaracesHaveFeatDataTables()
        {
            var metaraceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.MetaraceGroups);
            var allMetaraces = metaraceGroups[GroupConstants.All];

            foreach (var metarRace in allMetaraces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, metarRace);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllMetaraceSpeciesHaveFeatDataTables()
        {
            var metaraceSpecies = new[]
            {
                RaceConstants.Metaraces.Species.Black,
                RaceConstants.Metaraces.Species.Blue,
                RaceConstants.Metaraces.Species.Brass,
                RaceConstants.Metaraces.Species.Bronze,
                RaceConstants.Metaraces.Species.Copper,
                RaceConstants.Metaraces.Species.Gold,
                RaceConstants.Metaraces.Species.Green,
                RaceConstants.Metaraces.Species.Red,
                RaceConstants.Metaraces.Species.Silver,
                RaceConstants.Metaraces.Species.White,
            };

            foreach (var species in metaraceSpecies)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, species);
                var featsData = CollectionsMapper.Map(tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }
    }
}
