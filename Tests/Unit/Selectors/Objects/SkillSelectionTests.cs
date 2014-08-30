using NPCGen.Selectors.Interfaces.Objects;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors.Objects
{
    [TestFixture]
    public class SkillSelectionTests
    {
        private SkillSelection selection;

        [SetUp]
        public void Setup()
        {
            selection = new SkillSelection();
        }

        [Test]
        public void SkillSelectionInitialized()
        {
            Assert.That(selection.ArmorCheckPenalty, Is.False);
            Assert.That(selection.BaseStatName, Is.Empty);
        }
    }
}