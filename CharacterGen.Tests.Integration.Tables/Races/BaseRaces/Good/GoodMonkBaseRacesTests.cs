using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodMonkBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 2, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(3, 3, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(4, 13, RaceConstants.BaseRaces.HighElf)]
        [TestCase(14, 18, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(19, 19, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(20, 20, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(21, 25, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(26, 98, RaceConstants.BaseRaces.Human)]
        [TestCase(99, 99, RaceConstants.BaseRaces.HoundArchon)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Centaur)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}