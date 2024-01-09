using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.Metaraces.Good
{
    [TestFixture]
    public class GoodPaladinMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Good, CharacterClassConstants.Paladin); }
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