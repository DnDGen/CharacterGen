using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilRogueMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilRogueMetaraces";
        }

        [Test]
        public void EvilRogueEmptyPercentile()
        {
            AssertEmpty(1, 94);
        }

        [Test]
        public void EvilRogueWereratPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Wererat, 95, 96);
        }

        [Test]
        public void EvilRogueWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 97);
        }

        [Test]
        public void EvilRogueHalfFiendPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfFiend, 98, 99);
        }

        [Test]
        public void EvilRogueHalfDragonPercentile()
        {
            AssertContent(RaceConstants.Metaraces.HalfDragon, 100);
        }
    }
}