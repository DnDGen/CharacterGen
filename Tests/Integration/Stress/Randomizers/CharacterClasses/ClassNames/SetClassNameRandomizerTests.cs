using System;
using Ninject;
using NPCGen.Generators.Interfaces.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class SetClassNameRandomizerTests : StressTests
    {
        [Inject]
        public ISetClassNameRandomizer SetClassNameRandomizer { get; set; }
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
            SetClassNameRandomizer.SetClassName = Random.Next().ToString();
            var alignment = GetNewAlignment();

            var className = SetClassNameRandomizer.Randomize(alignment);
            Assert.That(className, Is.EqualTo(SetClassNameRandomizer.SetClassName));
        }
    }
}