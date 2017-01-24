using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Tables;
using CharacterGen.Races;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Tables.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWizardBaseRacesTests : PercentileTests
    {
        protected override string tableName
        {
            get { return string.Format(TableNameConstants.Formattable.Percentile.GOODNESSCLASSBaseRaces, AlignmentConstants.Good, CharacterClassConstants.Wizard); }
        }

        [Test]
        public override void TableIsComplete()
        {
            AssertTableIsComplete();
        }

        [TestCase(1, 1, RaceConstants.BaseRaces.Aasimar)]
        [TestCase(2, 2, RaceConstants.BaseRaces.HillDwarf)]
        [TestCase(3, 7, RaceConstants.BaseRaces.GrayElf)]
        [TestCase(8, 39, RaceConstants.BaseRaces.HighElf)]
        [TestCase(40, 40, RaceConstants.BaseRaces.Pixie)]
        [TestCase(41, 41, RaceConstants.BaseRaces.StormGiant)]
        [TestCase(42, 42, RaceConstants.BaseRaces.WoodElf)]
        [TestCase(43, 43, RaceConstants.BaseRaces.ForestGnome)]
        [TestCase(44, 48, RaceConstants.BaseRaces.RockGnome)]
        [TestCase(49, 58, RaceConstants.BaseRaces.HalfElf)]
        [TestCase(59, 63, RaceConstants.BaseRaces.LightfootHalfling)]
        [TestCase(64, 64, RaceConstants.BaseRaces.DeepHalfling)]
        [TestCase(65, 67, RaceConstants.BaseRaces.TallfellowHalfling)]
        [TestCase(68, 68, RaceConstants.BaseRaces.HalfOrc)]
        [TestCase(69, 99, RaceConstants.BaseRaces.Human)]
        [TestCase(100, 100, RaceConstants.BaseRaces.Svirfneblin)]
        public override void Percentile(int lower, int upper, string content)
        {
            base.Percentile(lower, upper, content);
        }
    }
}