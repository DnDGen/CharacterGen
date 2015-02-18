using Moq;
using NPCGen.Selectors;
using NPCGen.Selectors.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Unit.Selectors
{
    [TestFixture]
    public class SkillSelectorTests
    {
        private ISkillSelector skillSelector;
        private Mock<ICollectionsSelector> mockInnerSelector;

        [SetUp]
        public void Setup()
        {
            mockInnerSelector = new Mock<ICollectionsSelector>();
            skillSelector = new SkillSelector(mockInnerSelector.Object);
        }

        [Test]
        public void GetCollectionFromInnerCollectionSelectorWithArmorCheckPenalty()
        {
            var skillData = new[]
            {
                "base stat",
                "true"
            };

            mockInnerSelector.Setup(s => s.SelectFrom(INVALID"SkillData", "skill")).Returns(skillData);

            var selection = skillSelector.SelectFor("skill");
            Assert.That(selection.ArmorCheckPenalty, Is.True);
            Assert.That(selection.BaseStatName, Is.EqualTo("base stat"));
        }

        [Test]
        public void GetCollectionFromInnerCollectionSelectorWithoutArmorCheckPenalty()
        {
            var skillData = new[]
            {
                "base stat",
                "false"
            };

            mockInnerSelector.Setup(s => s.SelectFrom(INVALID"SkillData", "skill")).Returns(skillData);

            var selection = skillSelector.SelectFor("skill");
            Assert.That(selection.ArmorCheckPenalty, Is.False);
            Assert.That(selection.BaseStatName, Is.EqualTo("base stat"));
        }
    }
}