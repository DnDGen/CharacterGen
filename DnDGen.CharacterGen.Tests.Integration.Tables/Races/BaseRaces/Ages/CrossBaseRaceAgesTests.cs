using DnDGen.CharacterGen.Tables;
using DnDGen.Infrastructure.Mappers.Collections;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class CrossBaseRaceAgesTests : TableTests
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
        public void AllBaseRacesHaveAgeTables()
        {
            var baseRaceGroups = collectionsMapper.Map(Config.Name, TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, baseRace);
                var ages = collectionsMapper.Map(Config.Name, tableName);

                Assert.That(ages, Is.Not.Null);
                Assert.That(ages, Is.Not.Empty);
            }
        }
    }
}