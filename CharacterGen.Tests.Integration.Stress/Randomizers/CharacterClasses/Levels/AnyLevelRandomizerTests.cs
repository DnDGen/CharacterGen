using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class AnyLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Any)]
        public ILevelRandomizer AnyLevelRandomizer { get; set; }

        [TestCase("AnyLevelRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = LevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(1, 20));
        }
    }
}