using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilExpertMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Expert); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 97, RaceConstants.Metaraces.None)]
        [TestCase(98, 98, RaceConstants.Metaraces.Ghost)]
        [TestCase(99, 99, RaceConstants.Metaraces.Vampire)]
        [TestCase(100, 100, RaceConstants.Metaraces.Werewolf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}
