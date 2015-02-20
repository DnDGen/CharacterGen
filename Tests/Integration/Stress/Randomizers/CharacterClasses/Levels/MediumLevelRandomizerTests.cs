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

        [TestCase("MediumLevelRandomizer")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = MediumLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange<Int32>(6, 10));
        }
    }
}