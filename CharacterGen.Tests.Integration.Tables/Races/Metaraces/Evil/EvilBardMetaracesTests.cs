using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBardMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Bard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 96, RaceConstants.Metaraces.None)]
        [TestCase(97, 97, RaceConstants.Metaraces.Ghost)]
        [TestCase(98, 98, RaceConstants.Metaraces.Vampire)]
        [TestCase(99, 99, RaceConstants.Metaraces.Lich)]
        [TestCase(100, 100, RaceConstants.Metaraces.Werewolf)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}