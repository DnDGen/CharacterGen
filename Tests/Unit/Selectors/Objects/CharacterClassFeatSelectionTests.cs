using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class CharacterClassFeatSelectionTests
    {
        private CharacterClassFeatSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new CharacterClassFeatSelection();
        }

        [Test]
        public void SelectionIsInitialized()
        {
            Assert.That(selection.FeatId, Is.Empty);
            Assert.That(selection.FocusType, Is.Empty);
            Assert.That(selection.Frequency, Is.Not.Null);
            Assert.That(selection.MinimumLevel, Is.EqualTo(0));
            Assert.That(selection.RequiredFeatIds, Is.Empty);
            Assert.That(selection.Strength, Is.EqualTo(0));
        }
    }
}