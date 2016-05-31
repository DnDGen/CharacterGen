using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class MediumLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.Medium)]
        public ILevelRandomizer MediumLevelRandomizer { get; set; }

        [TestCase("MediumLevelRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = MediumLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(6, 10));
        }
    }
}