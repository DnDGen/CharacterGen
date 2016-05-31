using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodRangerMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Good, CharacterClassConstants.Ranger); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 96, RaceConstants.Metaraces.None)]
        [TestCase(97, 97, RaceConstants.Metaraces.Ghost)]
        [TestCase(98, 98, RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(99, 99, RaceConstants.Metaraces.HalfDragon)]
        [TestCase(100, 100, RaceConstants.Metaraces.Werebear)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}