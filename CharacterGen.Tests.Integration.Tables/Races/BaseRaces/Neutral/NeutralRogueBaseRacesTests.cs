using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRogueBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Rogue); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.DeepDwarf)]
        [TestCase(2, 4, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(5, 8, RaceConstants.BaseRaces.HighElf)]
        [TestCase(9, 9, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(10, 10, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(11, 25, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(26, 53, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(54, 58, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(59, 63, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(64, 73, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(74, 97, RaceConstants.BaseRaces.Human)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Doppelganger)]
        [TestCase(99, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}