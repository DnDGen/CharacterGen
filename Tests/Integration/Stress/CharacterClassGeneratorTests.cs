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

        [TestCase("CharacterClassGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var alignment = GetNewAlignment();

            var characterClass = CharacterClassGenerator.GenerateWith(alignment, LevelRandomizer, ClassNameRandomizer);
            Assert.That(classNames, Contains.Item(characterClass.ClassName));
            Assert.That(characterClass.Level, Is.Positive);
        }
    }
}