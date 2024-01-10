using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilWizardMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 91, RaceConstants.Metaraces.None)]
        [TestCase(92, 92, RaceConstants.Metaraces.Mummy)]
        [TestCase(93, 93, RaceConstants.Metaraces.Ghost)]
        [TestCase(94, 94, RaceConstants.Metaraces.Vampire)]
        [TestCase(95, 96, RaceConstants.Metaraces.Lich)]
        [TestCase(97, 97, RaceConstants.Metaraces.Wererat)]
        [TestCase(98, 98, RaceConstants.Metaraces.Werewolf)]
        [TestCase(99, 99, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(100, 100, RaceConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}