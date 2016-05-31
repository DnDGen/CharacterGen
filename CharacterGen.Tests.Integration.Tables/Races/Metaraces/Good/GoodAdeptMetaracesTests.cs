using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodAdeptMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Good, CharacterClassConstants.Adept); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 95, RaceConstants.Metaraces.None)]
        [TestCase(96, 96, RaceConstants.Metaraces.Ghost)]
        [TestCase(97, 98, RaceConstants.Metaraces.HalfCelestial)]
        [TestCase(99, 99, RaceConstants.Metaraces.HalfDragon)]
        [TestCase(100, 100, RaceConstants.Metaraces.Werebear)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
