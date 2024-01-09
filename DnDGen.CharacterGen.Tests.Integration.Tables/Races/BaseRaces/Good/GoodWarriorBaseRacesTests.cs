using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWarriorBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Warrior); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 3, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(4, 33, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(34, 41, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(42, 42, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(43, 47, RaceConstants.BaseRaces.HighElf)]
        [TestCase(48, 48, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(49, 50, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(51, 51, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(52, 52, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(53, 57, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(58, 100, RaceConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}