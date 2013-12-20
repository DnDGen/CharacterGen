using Ninject;
using NPCGen.Core.Data.Alignments;
using NPCGen.Core.Generation.Factories;
using NPCGen.Core.Generation.Factories.Interfaces;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Duration.Generation.Factories
{
    [TestFixture]
    public class CharacterClassFactoryTests : DurationTest
    {
        [Inject]
        public ICharacterClassFactory CharacterClassFactory { get; set; }
        [Inject]
        public Alignment Alignment { get; set; }

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
        public void CharacterClassGeneration()
        {
            CharacterClassFactory.CreatePrototypeWith(Alignment, LevelRandomizer, ClassNameRandomizer);
            AssertDuration();
        }
    }
}