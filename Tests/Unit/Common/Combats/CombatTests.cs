using NPCGen.Common.Combats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Combats
{
    [TestFixture]
    public class CombatTests
    {
        private Combat combat;

        [SetUp]
        public void Setup()
        {
            combat = new Combat();
        }

        [Test]
        public void CombatInitialized()
        {
            Assert.Fail();
        }
    }
}