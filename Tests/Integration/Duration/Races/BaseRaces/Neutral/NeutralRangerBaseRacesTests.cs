using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Tables.Races.BaseRaces.Neutral
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
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 1);
        }

        [Test]
        public void NeutralRangerHighElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HighElf, 2, 6);
        }

        [Test]
        public void NeutralRangerWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 7);
        }

        [Test]
        public void NeutralRangerWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 8, 36);
        }

        [Test]
        public void NeutralRangerForestGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.ForestGnome, 37);
        }

        [Test]
        public void NeutralRangerRockGnomePercentile()
        {
            AssertContent(RaceConstants.BaseRaces.RockGnome, 38);
        }

        [Test]
        public void NeutralRangerHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 39, 55);
        }

        [Test]
        public void NeutralRangerLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 56);
        }

        [Test]
        public void NeutralRangerTallfellowHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.TallfellowHalfling, 57);
        }

        [Test]
        public void NeutralRangerHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 58, 67);
        }

        [Test]
        public void NeutralRangerHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 68, 96);
        }

        [Test]
        public void NeutralRangerLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 97, 98);
        }

        [Test]
        public void NeutralRangerEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}