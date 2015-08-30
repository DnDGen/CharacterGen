using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
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

        [Test]
        public void PaladinHappensWhenLawfulGood()
        {
            var setAlignmentRandomizer = GetNewInstanceOf<ISetAlignmentRandomizer>();
            setAlignmentRandomizer.SetAlignment.Lawfulness = AlignmentConstants.Lawful;
            setAlignmentRandomizer.SetAlignment.Goodness = AlignmentConstants.Good;

            AlignmentRandomizer = setAlignmentRandomizer;

            CharacterClass characterClass;

            do characterClass = GenerateClass();
            while (TestShouldKeepRunning() && characterClass.ClassName != CharacterClassConstants.Paladin);

            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Paladin));
        }

        [Test]
        public void PaladinHappensWhenSetToPaladin()
        {
            var setAlignmentRandomizer = GetNewInstanceOf<ISetAlignmentRandomizer>();
            setAlignmentRandomizer.SetAlignment.Lawfulness = AlignmentConstants.Lawful;
            setAlignmentRandomizer.SetAlignment.Goodness = AlignmentConstants.Good;

            AlignmentRandomizer = setAlignmentRandomizer;

            var setClassRandomizer = GetNewInstanceOf<ISetClassNameRandomizer>();
            setClassRandomizer.SetClassName = CharacterClassConstants.Paladin;

            ClassNameRandomizer = setClassRandomizer;

            CharacterClass characterClass;

            do characterClass = GenerateClass();
            while (TestShouldKeepRunning() && characterClass.ClassName != CharacterClassConstants.Paladin);

            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Paladin));
        }
    }
}