using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilRangerMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Ranger); }
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
        [TestCase(95, 95, RaceConstants.Metaraces.Lich)]
        [TestCase(96, 96, RaceConstants.Metaraces.Wererat)]
        [TestCase(97, 98, RaceConstants.Metaraces.Werewolf)]
        [TestCase(99, 99, RaceConstants.Metaraces.HalfFiend)]
        [TestCase(100, 100, RaceConstants.Metaraces.HalfDragon)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}