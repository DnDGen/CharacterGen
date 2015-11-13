using CharacterGen.Common.CharacterClasses;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterClassGeneratorTests : StressTests
    {
        [TestCase("CharacterClassGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var characterClass = GenerateClass();
            Assert.That(characterClass.ClassName, Is.Not.Empty);
            Assert.That(characterClass.Level, Is.Positive);
        }

        private CharacterClass GenerateClass()
        {
            var alignment = GetNewAlignment();
            return CharacterClassGenerator.GenerateWith(alignment, LevelRandomizer, ClassNameRandomizer);
        }
    }
}