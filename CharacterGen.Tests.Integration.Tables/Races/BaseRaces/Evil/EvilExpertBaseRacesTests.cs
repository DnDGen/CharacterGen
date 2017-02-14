using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilExpertBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Expert); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.HighElf)]
        [TestCase(2, 2, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(3, 17, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(18, 18, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(19, 19, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(20, 20, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(21, 22, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(23, 99, RaceConstants.BaseRaces.Human)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Tiefling)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
