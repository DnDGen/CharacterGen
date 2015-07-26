using CharacterGen.Generators.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class LowLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Low)]
        public ILevelRandomizer LowLevelRandomizer { get; set; }

        [TestCase("LowLevelRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = LowLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange<Int32>(1, 5));
        }
    }
}