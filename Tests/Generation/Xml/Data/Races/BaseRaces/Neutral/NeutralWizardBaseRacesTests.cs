using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralWizardBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralWizardBaseRaces";
        }

        [Test]
        public void NeutralWizardGrayElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void NeutralWizardHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 2, 26);
        }

        [Test]
        public void NeutralWizardWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 27, 28);
        }

        [Test]
        public void NeutralWizardRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 29);
        }

        [Test]
        public void NeutralWizardHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 30, 44);
        }

        [Test]
        public void NeutralWizardLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 45, 47);
        }

        [Test]
        public void NeutralWizardTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 48, 49);
        }

        [Test]
        public void NeutralWizardHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 50);
        }

        [Test]
        public void NeutralWizardHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 51, 97);
        }

        [Test]
        public void NeutralWizardDoppelgangerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralWizardEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}