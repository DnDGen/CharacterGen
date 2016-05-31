using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodExpertBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Expert); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(2, 6, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(7, 11, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(12, 36, RaceConstants.BaseRaces.HighElf)]
        [TestCase(37, 37, RaceConstants.BaseRaces.WildElf)]
        [TestCase(38, 38, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(39, 39, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(40, 44, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(45, 53, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(54, 54, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(55, 55, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(56, 57, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(58, 97, RaceConstants.BaseRaces.Human)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(99, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
