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
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void NeutralWizardHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 2, 26);
        }

        [Test]
        public void NeutralWizardWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 27, 28);
        }

        [Test]
        public void NeutralWizardRockGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.RockGnome, 29);
        }

        [Test]
        public void NeutralWizardHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 30, 44);
        }

        [Test]
        public void NeutralWizardLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 45, 47);
        }

        [Test]
        public void NeutralWizardTallfellowHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.TallfellowHalfling, 48, 49);
        }

        [Test]
        public void NeutralWizardHalfOrcPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HalfOrc, 50);
        }

        [Test]
        public void NeutralWizardHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 51, 97);
        }

        [Test]
        public void NeutralWizardDoppelgangerPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralWizardEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}