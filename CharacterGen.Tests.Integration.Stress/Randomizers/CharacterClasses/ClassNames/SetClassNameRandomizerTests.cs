using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTests
    {
        [Inject]
        public ISetClassNameRandomizer SetClassNameRandomizer { get; set; }
        [Inject, Named(ClassNameRandomizerTypeConstants.AnyNPC)]
        public IClassNameRandomizer AnyNPCClassNameRandomizer { get; set; }

        [TearDown]
        public void TearDown()
        {
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.AnyPlayer);
        }

        [Test]
        public void StressClassName()
        {
            Stress(AssertClassName);
        }

        protected void AssertClassName()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            SetClassNameRandomizer.SetClassName = characterClass.Name;

            var className = SetClassNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(SetClassNameRandomizer.SetClassName));
        }

        [Test]
        public void StressNPCSetClassName()
        {
            ClassNameRandomizer = AnyNPCClassNameRandomizer;
            Stress(AssertClassName);
        }
    }
}