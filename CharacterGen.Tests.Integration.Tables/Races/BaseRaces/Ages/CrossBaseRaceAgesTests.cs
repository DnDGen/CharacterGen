using CharacterGen.Domain.Mappers.Collections;
using CharacterGen.Domain.Tables;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Ages
{
    [TestFixture]
    public class CrossBaseRaceAgesTests : IntegrationTests
    {
        [Inject]
        internal CollectionsMapper CollectionsMapper { get; set; }

        [Test]
        public void AllBaseRacesHaveAgeTables()
        {
            var baseRaceGroups = CollectionsMapper.Map(TableNameConstants.Set.Collection.BaseRaceGroups);
            var allBaseRaces = baseRaceGroups[GroupConstants.All];

            foreach (var baseRace in allBaseRaces)
            {
                var tableName = string.Format(TableNameConstants.Formattable.Adjustments.RACEAges, baseRace);
                var ages = CollectionsMapper.Map(tableName);

                Assert.That(ages, Is.Not.Null);
                Assert.That(ages, Is.Not.Empty);
            }
        }
    }
}