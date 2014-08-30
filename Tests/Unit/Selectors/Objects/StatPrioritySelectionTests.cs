using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class StatPrioritySelectionTests
    {
        private StatPrioritySelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new StatPrioritySelection();
        }

        [Test]
        public void StatPrioritySelectionInitialized()
        {
            Assert.That(selection.First, Is.Empty);
            Assert.That(selection.Second, Is.Empty);
        }
    }
}