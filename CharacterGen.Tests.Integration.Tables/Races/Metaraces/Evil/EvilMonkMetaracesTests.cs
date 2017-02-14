using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilMonkMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Monk); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 93, RaceConstants.Metaraces.None)]
        [TestCase(94, 94, RaceConstants.Metaraces.Mummy)]
        [TestCase(95, 95, RaceConstants.Metaraces.Ghost)]
        [TestCase(96, 96, RaceConstants.Metaraces.Vampire)]
        [TestCase(97, 98, RaceConstants.Metaraces.Wererat)]
        [TestCase(99, 99, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(100, 100, RaceConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}