using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWarriorBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Warrior); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(3, 4, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(5, 5, RaceConstants.BaseRaces.HighElf)]
        [TestCase(6, 7, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(8, 12, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(13, 13, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(14, 14, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(15, 23, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(24, 100, RaceConstants.BaseRaces.Human)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}