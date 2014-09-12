using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : StressTests
    {
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            SetLevelRandomizer.SetLevel = Random.Next();
            var level = SetLevelRandomizer.Randomize();
            Assert.That(level, Is.EqualTo(SetLevelRandomizer.SetLevel));
        }
    }
}