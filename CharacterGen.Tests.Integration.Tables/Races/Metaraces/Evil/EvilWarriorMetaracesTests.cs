using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilWarriorMetaracesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSMetaraces, AlignmentConstants.Evil, CharacterClassConstants.Warrior); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 94, RaceConstants.Metaraces.None)]
        [TestCase(95, 95, RaceConstants.Metaraces.Ghost)]
        [TestCase(96, 96, RaceConstants.Metaraces.Vampire)]
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