using CharacterGen.Selectors.Objects;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Selectors.Objects
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
        }
    }
}