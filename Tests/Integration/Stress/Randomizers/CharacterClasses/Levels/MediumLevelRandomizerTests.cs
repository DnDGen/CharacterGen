using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Medium)]
        public ILevelRandomizer MediumLevelRandomizer { get; set; }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var level = MediumLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange<Int32>(6, 10));
        }
    }
}