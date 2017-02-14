using CharacterGen.Domain.Selectors.Selections;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Selectors.Selections
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
            Assert.That(selection.BaseStatName, Is.Empty);
            Assert.That(selection.SkillName, Is.Empty);
            Assert.That(selection.RandomFociQuantity, Is.EqualTo(0));
        }
    }
}