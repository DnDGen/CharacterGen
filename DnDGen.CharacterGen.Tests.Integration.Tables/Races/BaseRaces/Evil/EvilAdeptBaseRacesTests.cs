using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 3, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 4, RaceConstants.BaseRaces.HighElf)]
        [TestCase(5, 5, RaceConstants.BaseRaces.WildElf)]
        [TestCase(6, 8, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(9, 18, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(19, 20, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(21, 21, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(22, 22, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(23, 25, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 99, RaceConstants.BaseRaces.Human)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Tiefling)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
