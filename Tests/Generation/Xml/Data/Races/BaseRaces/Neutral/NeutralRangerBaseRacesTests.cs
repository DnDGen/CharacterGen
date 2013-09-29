using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralRangerBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralRangerBaseRaces";
        }

        [Test]
        public void NeutralRangerHillDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HillDwarf, 1);
        }

        [Test]
        public void NeutralRangerHighElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HighElf, 2, 6);
        }

        [Test]
        public void NeutralRangerWildElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WildElf, 7);
        }

        [Test]
        public void NeutralRangerWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 8, 36);
        }

        [Test]
        public void NeutralRangerForestGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.ForestGnome, 37);
        }

        [Test]
        public void NeutralRangerRockGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.RockGnome, 38);
        }

        [Test]
        public void NeutralRangerHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 39, 55);
        }

        [Test]
        public void NeutralRangerLightfootHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.LightfootHalfling, 56);
        }

        [Test]
        public void NeutralRangerTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 57);
        }

        [Test]
        public void NeutralRangerHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 58, 67);
        }

        [Test]
        public void NeutralRangerHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 68, 96);
        }

        [Test]
        public void NeutralRangerLizardfolkPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Lizardfolk, 97, 98);
        }

        [Test]
        public void NeutralRangerEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}