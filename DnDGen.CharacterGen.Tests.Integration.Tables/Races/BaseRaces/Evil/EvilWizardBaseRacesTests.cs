using DnDGen.CharacterGen.Alignments;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Tables;
using DnDGen.CharacterGen.Races;
using NUnit.Framework;

namespace DnDGen.CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
{
    [TestFixture]
    public class EvilWizardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Evil, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 10, RaceConstants.BaseRaces.HighElf)]
        [TestCase(11, 11, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(12, 26, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(27, 27, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(28, 28, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(29, 76, RaceConstants.BaseRaces.Human)]
        [TestCase(77, 77, RaceConstants.BaseRaces.Githyanki)]
        [TestCase(78, 78, RaceConstants.BaseRaces.DeathSlaad)]
        [TestCase(79, 79, RaceConstants.BaseRaces.YuanTiPureblood)]
        [TestCase(80, 80, RaceConstants.BaseRaces.YuanTiHalfblood)]
        [TestCase(81, 81, RaceConstants.BaseRaces.YuanTiAbomination)]
        [TestCase(82, 83, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(84, 84, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(85, 94, RaceConstants.BaseRaces.Drow)]
        [TestCase(95, 95, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(96, 96, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(97, 97, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(98, 99, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Rakshasa)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}