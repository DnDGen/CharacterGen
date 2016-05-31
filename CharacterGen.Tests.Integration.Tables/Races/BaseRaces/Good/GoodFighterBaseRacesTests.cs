using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodFighterBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Fighter); }
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
        [TestCase(58, 97, RaceConstants.BaseRaces.Human)]
        [TestCase(98, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}