using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Good
{
    [TestFixture]
    public class GoodWizardBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "GoodWizardBaseRaces";
        }

        [Test]
        public void GoodWizardAasimarPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodWizardHillDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HillDwarf, 2);
        }

        [Test]
        public void GoodWizardGrayElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.GrayElf, 3, 7);
        }

        [Test]
        public void GoodWizardHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 8, 41);
        }

        [Test]
        public void GoodWizardWoodElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WoodElf, 42);
        }

        [Test]
        public void GoodWizardForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 43);
        }

        [Test]
        public void GoodWizardRockGnomeDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.RockGnome, 44, 48);
        }

        [Test]
        public void GoodWizardHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 49, 58);
        }

        [Test]
        public void GoodWizardLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 59, 63);
        }

        [Test]
        public void GoodWizardDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 64);
        }

        [Test]
        public void GoodWizardTallfellowHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.TallfellowHalfling, 65, 67);
        }

        [Test]
        public void GoodWizardHalfOrcPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HalfOrc, 68);
        }

        [Test]
        public void GoodWizardHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 69, 96);
        }

        [Test]
        public void GoodWizardSvirfneblinPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Svirfneblin, 97);
        }

        [Test]
        public void GoodWizardEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}