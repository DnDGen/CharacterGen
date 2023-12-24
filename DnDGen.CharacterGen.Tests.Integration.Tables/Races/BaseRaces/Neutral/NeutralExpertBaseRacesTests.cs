using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralExpertBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Expert); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 3, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 5, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(6, 15, RaceConstants.BaseRaces.HighElf)]
        [TestCase(16, 16, RaceConstants.BaseRaces.WildElf)]
        [TestCase(17, 21, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(22, 23, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(24, 33, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(34, 36, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(37, 37, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(38, 38, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(39, 40, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(41, 100, RaceConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
