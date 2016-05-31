using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralAdeptBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 15, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(16, 25, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(26, 26, RaceConstants.BaseRaces.MountainDwarf)]
        [TestCase(27, 27, RaceConstants.BaseRaces.HighElf)]
        [TestCase(28, 28, RaceConstants.BaseRaces.WildElf)]
        [TestCase(29, 38, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(39, 39, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(40, 48, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(49, 58, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(59, 59, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(60, 60, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(61, 62, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(63, 90, RaceConstants.BaseRaces.Human)]
        [TestCase(91, 97, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Doppelganger)]
        [TestCase(99, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
