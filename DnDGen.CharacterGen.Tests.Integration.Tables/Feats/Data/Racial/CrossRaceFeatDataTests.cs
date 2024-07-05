using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Mappers.Collections;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Feats.Data.Racial
{
    [TestFixture]
    public class CrossRaceFeatDataTests : TableTests
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
        public void AllBaseRacesHaveFeatDataTables()
        {
            var baseRaceGroups = collectionsMapper.Map(Config.Name, TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, baseRace);
                var featsData = collectionsMapper.Map(Config.Name, tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }

        [Test]
        public void AllMetaracesHaveFeatDataTables()
        {
            var metaraceGroups = collectionsMapper.Map(Config.Name, TableNameConstants.Set.Collection.MetaraceGroups);
            var allMetaraces = metaraceGroups[GroupConstants.All];

            foreach (var metarRace in allMetaraces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Collection.RACEFeatData, metarRace);
                var featsData = collectionsMapper.Map(Config.Name, tableName);

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
                var featsData = collectionsMapper.Map(Config.Name, tableName);

                Assert.That(featsData, Is.Not.Null);
            }
        }
    }
}
