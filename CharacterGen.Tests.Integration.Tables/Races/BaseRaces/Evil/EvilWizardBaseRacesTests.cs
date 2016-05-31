using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Evil
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
        [TestCase(29, 78, RaceConstants.BaseRaces.Human)]
        [TestCase(79, 80, RaceConstants.BaseRaces.Hobgoblin)]
        [TestCase(81, 81, RaceConstants.BaseRaces.Tiefling)]
        [TestCase(82, 91, RaceConstants.BaseRaces.Drow)]
        [TestCase(92, 92, RaceConstants.BaseRaces.Gnoll)]
        [TestCase(93, 93, RaceConstants.BaseRaces.Bugbear)]
        [TestCase(94, 94, RaceConstants.BaseRaces.MindFlayer)]
        [TestCase(95, 96, RaceConstants.BaseRaces.OgreMage)]
        [TestCase(97, 100, EmptyContent)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}