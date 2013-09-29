using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralFighterBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralFighterBaseRaces";
        }

        [Test]
        public void NeutralFighterDeepDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.DeepDwarf, 1, 10);
        }

        [Test]
        public void NeutralFighterHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 11, 29);
        }

        [Test]
        public void NeutralFighterMountainDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.MountainDwarf, 30, 34);
        }

        [Test]
        public void NeutralFighterHighElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HighElf, 35);
        }

        [Test]
        public void NeutralFighterWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 36, 41);
        }

        [Test]
        public void NeutralFighterHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 42, 46);
        }

        [Test]
        public void NeutralFighterLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 47);
        }

        [Test]
        public void NeutralFighterDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 48);
        }

        [Test]
        public void NeutralFighterHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 49, 58);
        }

        [Test]
        public void NeutralFighterHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 59, 96);
        }

        [Test]
        public void NeutralFighterLizardfolkPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Lizardfolk, 97);
        }

        [Test]
        public void NeutralFighterDoppelgangerPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralFighterEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}