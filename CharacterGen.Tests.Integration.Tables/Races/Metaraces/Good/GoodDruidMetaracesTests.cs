using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodDruidMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Good, CharacterClassConstants.Druid); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 98, RaceConstants.Metaraces.None)]
        [TestCase(99, 99, RaceConstants.Metaraces.Ghost)]
        [TestCase(100, 100, RaceConstants.Metaraces.HalfCelestial)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}