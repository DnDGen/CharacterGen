using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.BaseRaces.Good
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
            AssertContent(RaceConstants.BaseRaces.Aasimar, 1);
        }

        [Test]
        public void GoodWizardHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 2);
        }

        [Test]
        public void GoodWizardGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 3, 7);
        }

        [Test]
        public void GoodWizardHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 8, 41);
        }

        [Test]
        public void GoodWizardWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 42);
        }

        [Test]
        public void GoodWizardForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 43);
        }

        [Test]
        public void GoodWizardRockGnomeDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 44, 48);
        }

        [Test]
        public void GoodWizardHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 49, 58);
        }

        [Test]
        public void GoodWizardLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 59, 63);
        }

        [Test]
        public void GoodWizardDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 64);
        }

        [Test]
        public void GoodWizardTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 65, 67);
        }

        [Test]
        public void GoodWizardHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 68);
        }

        [Test]
        public void GoodWizardHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 69, 96);
        }

        [Test]
        public void GoodWizardSvirfneblinPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Svirfneblin, 97);
        }

        [Test]
        public void GoodWizardEmptyPercentile()
        {
            AssertEmpty(98, 100);
        }
    }
}