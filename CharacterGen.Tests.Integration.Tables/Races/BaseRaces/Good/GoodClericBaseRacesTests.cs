using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodClericBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(2, 2, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 22, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(23, 24, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(25, 25, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(26, 35, RaceConstants.BaseRaces.HighElf)]
        [TestCase(36, 40, RaceConstants.BaseRaces.WildElf)]
        [TestCase(41, 41, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(42, 42, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(43, 51, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(52, 56, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(57, 66, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(67, 67, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(68, 69, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(70, 70, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(71, 95, RaceConstants.BaseRaces.Human)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(97, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}