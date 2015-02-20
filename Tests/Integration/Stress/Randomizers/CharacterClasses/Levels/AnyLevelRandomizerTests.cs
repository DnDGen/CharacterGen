using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Any)]
        public ILevelRandomizer AnyLevelRandomizer { get; set; }

        [TestCase("AnyLevelRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = LevelRandomizer.Randomize();
            Assert.That(level, Is.InRange<Int32>(1, 20));
        }
    }
}