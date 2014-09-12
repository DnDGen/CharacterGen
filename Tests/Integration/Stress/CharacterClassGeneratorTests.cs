using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NUnit.Framework;

namespace NPCGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterClassGeneratorTests : StressTests
    {
        private IEnumerable<String> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = CharacterClassConstants.GetClassNames();
        }

        [Test]
        public override void Stress()
        {
            do MakeAssertions();
            while (TestShouldKeepRunning());

            AssertIterations();
        }

        private void MakeAssertions()
        {
            var alignment = GetNewAlignment();

            var characterClass = CharacterClassGenerator.GenerateWith(alignment, LevelRandomizer, ClassNameRandomizer);
            Assert.That(classNames, Contains.Item(characterClass.ClassName));
            Assert.That(characterClass.Level, Is.Positive);
        }
    }
}