using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class HighLevelRandomizerTests : StressTests
    {
        [Inject, Named(LevelRandomizerTypeConstants.High)]
        public ILevelRandomizer HighLevelRandomizer { get; set; }

        protected override void MakeAssertions()
        {
            var level = HighLevelRandomizer.Randomize();
            Assert.That(level, Is.InRange<Int32>(11, 15));
        }
    }
}