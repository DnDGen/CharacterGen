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
            AssertContent(RaceConstants.BaseRaces.DeepDwarf, 1);
        }

        [Test]
        public void NeutralBarbarianHillDwarfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HillDwarf, 2);
        }

        [Test]
        public void NeutralBarbarianWildElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WildElf, 3, 13);
        }

        [Test]
        public void NeutralBarbarianWoodElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.WoodElf, 14);
        }

        [Test]
        public void NeutralBarbarianHalfElfPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfElf, 15, 16);
        }

        [Test]
        public void NeutralBarbarianLightfootHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.LightfootHalfling, 17, 18);
        }

        [Test]
        public void NeutralBarbarianDeepHalflingPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.DeepHalfling, 19);
        }

        [Test]
        public void NeutralBarbarianHalfOrcPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.HalfOrc, 20, 58);
        }

        [Test]
        public void NeutralBarbarianHumanPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Human, 59, 87);
        }

        [Test]
        public void NeutralBarbarianLizardfolkPercentile()
        {
            AssertContent(RaceConstants.BaseRaces.Lizardfolk, 88, 98);
        }

        [Test]
        public void NeutralBarbarianEmptyPercentile()
        {
            AssertEmpty(99, 100);
        }
    }
}