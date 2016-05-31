using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class VeryHighLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.VeryHigh)]
        public ILevelRandomizer VeryHighLevelRandomizer { get; set; }

        [TestCase("VeryHighLevelRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = VeryHighLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(16, 20));
        }
    }
}