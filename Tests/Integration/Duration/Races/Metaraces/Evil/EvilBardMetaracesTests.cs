using NPCGen.Core.Data.Races;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Generation.Xml.Data.Races.Metaraces.Evil
{
    [TestFixture]
    public class EvilBardMetaracesTests : PercentileTests
    {
        [SetUp]
        public void Setup()
        {
            tableName = "EvilBardMetaraces";
        }

        [Test]
        public void EvilBardEmptyPercentile()
        {
            AssertEmpty(1, 99);
        }

        [Test]
        public void EvilBardWerewolfPercentile()
        {
            AssertContent(RaceConstants.Metaraces.Werewolf, 100);
        }
    }
}