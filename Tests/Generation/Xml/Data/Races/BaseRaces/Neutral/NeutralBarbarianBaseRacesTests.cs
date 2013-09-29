using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.BaseRaces.Neutral
{
    [TestFixture]
    public class NeutralBarbarianBaseRacesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "NeutralBarbarianBaseRaces";
        }

        [Test]
        public void NeutralBarbarianDeepDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralBarbarianHillDwarfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.HillDwarf, 2);
        }

        [Test]
        public void NeutralBarbarianWildElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.WildElf, 3, 13);
        }

        [Test]
        public void NeutralBarbarianWoodElfPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.WoodElf, 14);
        }

        [Test]
        public void NeutralBarbarianHalfElfPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfElf, 15, 16);
        }

        [Test]
        public void NeutralBarbarianLightfootHalflingPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.LightfootHalfling, 17, 18);
        }

        [Test]
        public void NeutralBarbarianDeepHalflingPercentile()
        {
            AssertContentOnSingleRoll(RaceConstants.BaseRaces.DeepHalfling, 19);
        }

        [Test]
        public void NeutralBarbarianHalfOrcPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.HalfOrc, 20, 58);
        }

        [Test]
        public void NeutralBarbarianHumanPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Human, 59, 87);
        }

        [Test]
        public void NeutralBarbarianLizardfolkPercentile()
        {
            AssertContentIsInRange(RaceConstants.BaseRaces.Lizardfolk, 88, 98);
        }

        [Test]
        public void NeutralBarbarianEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}