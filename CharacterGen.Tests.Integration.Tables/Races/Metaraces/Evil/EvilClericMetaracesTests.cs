using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilClericMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Cleric); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 92, RaceConstants.Metaraces.None)]
        [TestCase(93, 93, RaceConstants.Metaraces.Ghost)]
        [TestCase(94, 94, RaceConstants.Metaraces.Vampire)]
        [TestCase(95, 95, RaceConstants.Metaraces.Lich)]
        [TestCase(96, 96, RaceConstants.Metaraces.Wererat)]
        [TestCase(97, 97, RaceConstants.Metaraces.Werewolf)]
        [TestCase(98, 99, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(100, 100, RaceConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}