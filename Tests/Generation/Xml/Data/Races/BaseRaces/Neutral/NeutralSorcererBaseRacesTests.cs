using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralSorcererBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralSorcererBaseRaces";
        }

        [Test]
        public void NeutralSorcererHillDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HillDwarf, 1);
        }

        [Test]
        public void NeutralSorcererHighElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HighElf, 2);
        }

        [Test]
        public void NeutralSorcererWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 3, 12);
        }

        [Test]
        public void NeutralSorcererWoodElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WoodElf, 13, 15);
        }

        [Test]
        public void NeutralSorcererRockGnomePercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.RockGnome, 16);
        }

        [Test]
        public void NeutralSorcererHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 17, 31);
        }

        [Test]
        public void NeutralSorcererLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 32, 41);
        }

        [Test]
        public void NeutralSorcererDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 42);
        }

        [Test]
        public void NeutralSorcererTallfellowHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.TallfellowHalfling, 43);
        }

        [Test]
        public void NeutralSorcererHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 44, 48);
        }

        [Test]
        public void NeutralSorcererHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 49, 95);
        }

        [Test]
        public void NeutralSorcererLizardfolkPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Lizardfolk, 96, 97);
        }

        [Test]
        public void NeutralSorcererDoppelgangerPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralSorcererEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}