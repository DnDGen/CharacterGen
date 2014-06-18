using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTests
    {
        [Inject]
        public SetClassNameRandomizer SetClassNameRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var data = GetNewDependentData();
            SetClassNameRandomizer.ClassName = data.CharacterClassPrototype.ClassName;

            var className = ClassNameRandomizer.Randomize(data.Alignment);
            Assert.That(className, Is.EqualTo(data.CharacterClassPrototype.ClassName));
        }
    }
}