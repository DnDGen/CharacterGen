using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
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
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1, 10);
        }

        [Test]
        public void NeutralFighterHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 11, 29);
        }

        [Test]
        public void NeutralFighterMountainDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MountainDwarf, 30, 34);
        }

        [Test]
        public void NeutralFighterHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 35);
        }

        [Test]
        public void NeutralFighterWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 36, 41);
        }

        [Test]
        public void NeutralFighterHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 42, 46);
        }

        [Test]
        public void NeutralFighterLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 47);
        }

        [Test]
        public void NeutralFighterDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 48);
        }

        [Test]
        public void NeutralFighterHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 49, 58);
        }

        [Test]
        public void NeutralFighterHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 59, 96);
        }

        [Test]
        public void NeutralFighterLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 97);
        }

        [Test]
        public void NeutralFighterDoppelgangerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralFighterEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}