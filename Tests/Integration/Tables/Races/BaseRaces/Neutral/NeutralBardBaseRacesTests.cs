using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using CharacterGen.Tables;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Neutral, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(RaceConstants.BaseRaces.HillDwarf, 2, 3)]
        [TestCase(RaceConstants.BaseRaces.GrayElf, 4, 5)]
        [TestCase(RaceConstants.BaseRaces.HighElf, 6, 15)]
        [TestCase(RaceConstants.BaseRaces.WoodElf, 17, 21)]
        [TestCase(RaceConstants.BaseRaces.RockGnome, 22, 23)]
        [TestCase(RaceConstants.BaseRaces.HalfElf, 24, 33)]
        [TestCase(RaceConstants.BaseRaces.LightfootHalfling, 34, 36)]
        [TestCase(RaceConstants.BaseRaces.HalfOrc, 39, 40)]
        [TestCase(RaceConstants.BaseRaces.Human, 41, 98)]
        [TestCase(EmptyContent, 99, 100)]
        public override void Percentile(string content, int lower, int upper)
        {
            base.Percentile(content, lower, upper);
        }

        [TestCase(RaceConstants.BaseRaces.DeepDwarf, 1)]
        [TestCase(RaceConstants.BaseRaces.WildElf, 16)]
        [TestCase(RaceConstants.BaseRaces.DeepHalfling, 37)]
        [TestCase(RaceConstants.BaseRaces.TallfellowHalfling, 38)]
        public override void Percentile(string content, int roll)
        {
            base.Percentile(content, roll);
        }
    }
}