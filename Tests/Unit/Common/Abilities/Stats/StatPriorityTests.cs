using NPCGen.Common.Abilities.Stats;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Common.Abilities.Stats
{
    [TestFixture]
    public class StatPriorityTests
    {
        private StatPriority statPriority;

        [SetUp]
        public void Setup()
        {
            statPriority = new StatPriority();
        }

        [Test]
        public void StatPriorityInitialized()
        {
            Assert.That(statPriority.FirstPriority, Is.Empty);
            Assert.That(statPriority.SecondPriority, Is.Empty);
        }
    }
}