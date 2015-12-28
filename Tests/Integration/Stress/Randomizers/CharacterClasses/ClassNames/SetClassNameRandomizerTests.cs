using CharacterGen.Generators.Randomizers.CharacterClasses;
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

        [TestCase("Set Class Name Randomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);

            SetClassNameRandomizer.SetClassName = characterClass.ClassName;

            var className = SetClassNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(SetClassNameRandomizer.SetClassName));
        }

        [Test]
        public void StressNPCSetClassName()
        {
            ClassNameRandomizer = AnyNPCClassNameRandomizer;
            Stress(AssertNPCSetClassName);
        }

        private void AssertNPCSetClassName()
        {
            var alignment = GetNewAlignment();
            var characterClass = GetNewCharacterClass(alignment);
            SetClassNameRandomizer.SetClassName = characterClass.ClassName;

            var baseRace = SetClassNameRandomizer.Randomize(alignment);
            Assert.That(baseRace, Is.EqualTo(SetClassNameRandomizer.SetClassName));
        }
    }
}