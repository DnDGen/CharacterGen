using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(2, 2, RaceConstants.BaseRaces.HighElf)]
        [TestCase(3, 12, RaceConstants.BaseRaces.WildElf)]
        [TestCase(13, 15, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(16, 16, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(17, 31, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(32, 41, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(42, 42, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(43, 43, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(44, 48, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(49, 95, RaceConstants.BaseRaces.Human)]
        [TestCase(96, 97, RaceConstants.BaseRaces.Lizardfolk)]
        [TestCase(98, 98, RaceConstants.BaseRaces.Doppelganger)]
        [TestCase(99, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}