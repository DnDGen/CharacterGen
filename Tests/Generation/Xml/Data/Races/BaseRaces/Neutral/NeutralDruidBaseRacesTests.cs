using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralDruidBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralDruidBaseRaces";
        }

        [Test]
        public void NeutralDruidGrayElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.GrayElf, 1);
        }

        [Test]
        public void NeutralDruidHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 2, 6);
        }

        [Test]
        public void NeutralDruidWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 7, 11);
        }

        [Test]
        public void NeutralDruidWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 12, 31);
        }

        [Test]
        public void NeutralDruidForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 32);
        }

        [Test]
        public void NeutralDruidHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 33, 37);
        }

        [Test]
        public void NeutralDruidLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 38);
        }

        [Test]
        public void NeutralDruidTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 39);
        }

        [Test]
        public void NeutralDruidHalfOrcPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HalfOrc, 40);
        }

        [Test]
        public void NeutralDruidHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 41, 88);
        }

        [Test]
        public void NeutralDruidLizardfolkPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Lizardfolk, 89, 98);
        }

        [Test]
        public void NeutralDruidEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}