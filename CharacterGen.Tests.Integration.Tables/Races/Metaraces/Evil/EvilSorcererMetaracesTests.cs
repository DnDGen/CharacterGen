using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilSorcererMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Sorcerer); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 90, RaceConstants.Metaraces.None)]
        [TestCase(91, 91, RaceConstants.Metaraces.Mummy)]
        [TestCase(92, 92, RaceConstants.Metaraces.Ghost)]
        [TestCase(93, 93, RaceConstants.Metaraces.Vampire)]
        [TestCase(94, 95, RaceConstants.Metaraces.Lich)]
        [TestCase(96, 96, RaceConstants.Metaraces.Wererat)]
        [TestCase(97, 97, RaceConstants.Metaraces.Werewolf)]
        [TestCase(98, 98, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(99, 100, RaceConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}