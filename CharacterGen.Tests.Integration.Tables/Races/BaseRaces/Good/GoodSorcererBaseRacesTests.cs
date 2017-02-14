using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(3, 3, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(4, 5, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(6, 6, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(7, 8, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(9, 11, RaceConstants.BaseRaces.HighElf)]
        [TestCase(12, 35, RaceConstants.BaseRaces.WildElf)]
        [TestCase(36, 36, RaceConstants.BaseRaces.HoundArchon)]
        [TestCase(37, 37, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(38, 38, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(39, 40, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(41, 45, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(46, 54, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(55, 55, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(56, 56, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(57, 58, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(59, 95, RaceConstants.BaseRaces.Human)]
        [TestCase(96, 97, RaceConstants.BaseRaces.Pixie)]
        [TestCase(98, 98, RaceConstants.BaseRaces.StormGiant)]
        [TestCase(99, 99, RaceConstants.BaseRaces.Svirfneblin)]
        [TestCase(100, 100, RaceConstants.BaseRaces.CloudGiant)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}