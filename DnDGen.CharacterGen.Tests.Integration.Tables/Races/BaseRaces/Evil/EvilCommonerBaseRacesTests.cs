using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilCommonerBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Commoner); }
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
        [TestCase(46, 96, RaceConstants.BaseRaces.Human)]
        [TestCase(97, 97, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(98, 98, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(99, 99, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(100, 100, RaceConstants.BaseRaces.WildElf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
