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
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 1);
        }

        [Test]
        public void NeutralSorcererHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 2);
        }

        [Test]
        public void NeutralSorcererWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 3, 12);
        }

        [Test]
        public void NeutralSorcererWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 13, 15);
        }

        [Test]
        public void NeutralSorcererRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 16);
        }

        [Test]
        public void NeutralSorcererHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 17, 31);
        }

        [Test]
        public void NeutralSorcererLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 32, 41);
        }

        [Test]
        public void NeutralSorcererDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 42);
        }

        [Test]
        public void NeutralSorcererTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 43);
        }

        [Test]
        public void NeutralSorcererHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 44, 48);
        }

        [Test]
        public void NeutralSorcererHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 49, 95);
        }

        [Test]
        public void NeutralSorcererLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 96, 97);
        }

        [Test]
        public void NeutralSorcererDoppelgangerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralSorcererEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}