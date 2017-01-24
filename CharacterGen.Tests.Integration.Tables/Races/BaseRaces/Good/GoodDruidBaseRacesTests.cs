using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodDruidBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(2, 11, RaceConstants.BaseRaces.HighElf)]
        [TestCase(12, 21, RaceConstants.BaseRaces.WildElf)]
        [TestCase(22, 31, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(32, 36, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(37, 37, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(38, 46, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(47, 47, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(48, 48, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(49, 49, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(50, 97, RaceConstants.BaseRaces.Human)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Pixie)]
        [TestCase(99, 99, RaceConstants.BaseRaces.CloudGiant)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}