using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.ClassNames;
using NPCGen.Tests.Integration.Common;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTest
    {
        [Inject]
        public SetClassNameRandomizer ClassNameRandomizer { get; set; }

        [Test]
        public void SetClassNameRandomizerAlwaysReturnsSetClassName()
        {
            while (TestShouldKeepRunning())
            {
                var data = GetNewInstanceOf<DependentDataCollection>();
                ClassNameRandomizer.ClassName = data.CharacterClassPrototype.ClassName;

                var className = ClassNameRandomizer.Randomize(data.Alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
                Assert.That(className, Is.EqualTo(ClassNameRandomizer.ClassName));
            }

            AssertIterations();
        }
    }
}