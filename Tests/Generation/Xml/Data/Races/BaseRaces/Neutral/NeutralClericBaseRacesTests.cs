using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralClericBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralClericBaseRaces";
        }

        [Test]
        public void NeutralClericDeepDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.DeepDwarf, 1, 15);
        }

        [Test]
        public void NeutralClericHillDwarfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HillDwarf, 16, 25);
        }

        [Test]
        public void NeutralClericMountainDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.MountainDwarf, 26);
        }

        [Test]
        public void NeutralClericHighElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HighElf, 27);
        }

        [Test]
        public void NeutralClericWildElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WildElf, 28);
        }

        [Test]
        public void NeutralClericWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 29, 38);
        }

        [Test]
        public void NeutralClericRockGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.RockGnome, 39);
        }

        [Test]
        public void NeutralClericHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 40, 48);
        }

        [Test]
        public void NeutralClericLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 49, 58);
        }

        [Test]
        public void NeutralClericDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 59);
        }

        [Test]
        public void NeutralClericTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 60);
        }

        [Test]
        public void NeutralClericHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 61, 62);
        }

        [Test]
        public void NeutralClericHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 63, 90);
        }

        [Test]
        public void NeutralClericLizardfolkPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Lizardfolk, 91, 97);
        }

        [Test]
        public void NeutralClericDoppelgangerPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralClericEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}