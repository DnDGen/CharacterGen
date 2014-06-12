using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
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
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1, 15);
        }

        [Test]
        public void NeutralClericHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 16, 25);
        }

        [Test]
        public void NeutralClericMountainDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.MountainDwarf, 26);
        }

        [Test]
        public void NeutralClericHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 27);
        }

        [Test]
        public void NeutralClericWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 28);
        }

        [Test]
        public void NeutralClericWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 29, 38);
        }

        [Test]
        public void NeutralClericRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 39);
        }

        [Test]
        public void NeutralClericHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 40, 48);
        }

        [Test]
        public void NeutralClericLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 49, 58);
        }

        [Test]
        public void NeutralClericDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 59);
        }

        [Test]
        public void NeutralClericTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 60);
        }

        [Test]
        public void NeutralClericHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 61, 62);
        }

        [Test]
        public void NeutralClericHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 63, 90);
        }

        [Test]
        public void NeutralClericLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 91, 97);
        }

        [Test]
        public void NeutralClericDoppelgangerPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Doppelganger, 98);
        }

        [Test]
        public void NeutralClericEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}