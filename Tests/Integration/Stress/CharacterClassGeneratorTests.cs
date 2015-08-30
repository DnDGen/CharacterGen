using CharacterGen.Common.CharacterClasses;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CharacterGen.Tests.Integration.Stress
{
    [TestFixture]
    public class CharacterClassGeneratorTests : StressTests
    {
        private IEnumerable<String> classNames;

        [SetUp]
        public void Setup()
        {
            classNames = new[] {
                CharacterClassConstants.Barbarian,
                CharacterClassConstants.Bard,
                CharacterClassConstants.Cleric,
                CharacterClassConstants.Druid,
                CharacterClassConstants.Fighter,
                CharacterClassConstants.Monk,
                CharacterClassConstants.Paladin,
                CharacterClassConstants.Ranger,
                CharacterClassConstants.Rogue,
                CharacterClassConstants.Sorcerer,
                CharacterClassConstants.Wizard
            };
        }

        [TestCase("CharacterClassGenerator")]
        public override void Stress(String stressSubject)
        {
            Stress();
        }

        protected override void MakeAssertions()
        {
            var characterClass = GenerateClass();
            Assert.That(classNames, Contains.Item(characterClass.ClassName));
            Assert.That(characterClass.Level, Is.Positive);
        }

        private CharacterClass GenerateClass()
        {
            var alignment = GetNewAlignment();
            return CharacterClassGenerator.GenerateWith(alignment, LevelRandomizer, ClassNameRandomizer);
        }

        [Test]
        public void PaladinHappens()
        {
            CharacterClass characterClass;

            do characterClass = GenerateClass();
            while (TestShouldKeepRunning() && characterClass.ClassName != CharacterClassConstants.Paladin);

            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Paladin));
        }
    }
}