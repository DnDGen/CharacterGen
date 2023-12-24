using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodCommonerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Commoner); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(11, 15, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(16, 25, RaceConstants.BaseRaces.HighElf)]
        [TestCase(26, 35, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(36, 45, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(46, 50, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(51, 55, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(56, 94, RaceConstants.BaseRaces.Human)]
        [TestCase(95, 95, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(96, 96, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(98, 98, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(99, 99, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(100, 100, RaceConstants.BaseRaces.WildElf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
