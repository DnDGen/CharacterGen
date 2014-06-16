using Ninject;
using NPCGen.Common.CharacterClasses;
using NPCGen.Generators.Interfaces;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterClassGeneratorTests : StressTests
    {
        [Inject]
        public ICharacterClassGenerator CharacterClassGenerator { get; set; }
        [Inject]
        public IClassNameRandomizer ClassNameRandomizer { get; set; }
        [Inject]
        public ILevelRandomizer LevelRandomizer { get; set; }

        [Test]
        public void StressCharacterClassGeneratorForPrototypes()
        {
            var classNames = CharacterClassConstants.GetClassNames();

            while (TestShouldKeepRunning())
            {
                var dependentData = GetNewInstanceOf<DependentDataCollection>();

                var prototype = CharacterClassGenerator.CreatePrototypeWith(dependentData.Alignment, LevelRandomizer, ClassNameRandomizer);
                Assert.That(classNames, Contains.Item(prototype.ClassName));
                Assert.That(prototype.Level, Is.GreaterThan(0));
            }

            AssertIterations();
        }

        [Test]
        public void StressCharacterClassGeneratorForCharacterClass()
        {
            while (TestShouldKeepRunning())
            {
                var dependentData = GetNewInstanceOf<DependentDataCollection>();
                var prototype = CharacterClassGenerator.CreatePrototypeWith(dependentData.Alignment, LevelRandomizer, ClassNameRandomizer);

                var characterClass = CharacterClassGenerator.CreateWith(prototype);
                Assert.That(characterClass.ClassName, Is.EqualTo(prototype.ClassName));
                Assert.That(characterClass.Level, Is.EqualTo(prototype.Level));
                Assert.That(characterClass.BaseAttack.BaseAttackBonus, Is.Not.Negative);
            }

            AssertIterations();
        }
    }
}