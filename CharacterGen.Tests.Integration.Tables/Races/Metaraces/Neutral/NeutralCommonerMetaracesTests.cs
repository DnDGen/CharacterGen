using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Neutral
{
    [TestFixture]
    public class NeutralCommonerMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Neutral, CharacterClassConstants.Commoner); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 99, RaceConstants.Metaraces.None)]
        [TestCase(100, 100, RaceConstants.Metaraces.Ghost)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}