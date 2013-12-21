using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Data.CharacterClasses;
using NPCGen.Core.Generation.Randomizers.CharacterClasses.ClassNames;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Generation.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTest
    {
        [Inject]
        public SetClassNameRandomizer ClassNameRandomizer { get; set; }

        [SetUp]
        public void Setup()
        {
            StartTest();
        }

        [TearDown]
        public void TearDown()
        {
            StopTest();
        }

        [Test]
        public void SetClassNameRandomizerReturnsClassName()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();
                var alignment = GetNewInstanceOf<Alignment>();
                ClassNameRandomizer.ClassName = prototype.ClassName;

                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(className, Is.Not.Null);
                Assert.That(className, Is.Not.Empty);
            }

            AssertIterations();
        }

        [Test]
        public void SetClassNameRandomizerAlwaysReturnsSetClassName()
        {
            while (TestShouldKeepRunning())
            {
                var prototype = GetNewInstanceOf<CharacterClassPrototype>();
                var alignment = GetNewInstanceOf<Alignment>();
                ClassNameRandomizer.ClassName = prototype.ClassName;

                var className = ClassNameRandomizer.Randomize(alignment);
                Assert.That(className, Is.EqualTo(ClassNameRandomizer.ClassName));
            }

            AssertIterations();
        }
    }
}