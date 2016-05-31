using System;
using Ninject;
using CharacterGen.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace CharacterGen.Tests.Integration.Stress.Randomizers.CharacterClasses.Levels
{
    [TestFixture]
    public class SetLevelRandomizerTests : StressTests
    {
        [Inject]
        public ISetLevelRandomizer SetLevelRandomizer { get; set; }
        [Inject]
        public Random Random { get; set; }

        [TestCase("SetLevelRandomizer")]
        public override void Stress(string stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            SetLevelRandomizer.SetLevel = Random.Next();
            var level = SetLevelRandomizer.Randomize();
            Assert.That(level, Is.EqualTo(SetLevelRandomizer.SetLevel));
        }
    }
}