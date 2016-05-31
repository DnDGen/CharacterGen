using CharacterGen.Randomizers.CharacterClasses;
using Ninject;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.High)]
        public ILevelRandomizer HighLevelRandomizer { get; set; }

        [TestCase("HighLevelRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var level = HighLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange(11, 15));
        }
    }
}