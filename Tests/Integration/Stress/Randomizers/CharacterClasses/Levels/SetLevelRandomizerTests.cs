using System;
using Ninject;
using NPCGen.Generators.Randomizers.CharacterClasses.Levels;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : StressTests
    {
        [Inject]
        public SetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        protected override void MakeAssertions()
        {
            SetLevelRandomizer.Level = Random.Next();
            var level = SetLevelRandomizer.Randomize();
            Assert.That(level, Is.EqualTo(SetLevelRandomizer.Level));
        }
    }
}