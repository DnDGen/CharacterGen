using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralDruidBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(2, 6, RaceConstants.BaseRaces.HighElf)]
        [TestCase(7, 11, RaceConstants.BaseRaces.WildElf)]
        [TestCase(12, 31, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(32, 32, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(33, 37, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(38, 38, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(39, 39, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(40, 40, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(41, 88, RaceConstants.BaseRaces.Human)]
        [TestCase(89, 89, RaceConstants.BaseRaces.Satyr)]
        [TestCase(90, 99, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Janni)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}