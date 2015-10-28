using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Generators.Randomizers.Alignments;
using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;
using System;
using System.Linq;

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

        [TearDown]
        public void TearDown()
        {
            AlignmentRandomizer = GetNewInstanceOf<IAlignmentRandomizer>(AlignmentRandomizerTypeConstants.Any);
            ClassNameRandomizer = GetNewInstanceOf<IClassNameRandomizer>(ClassNameRandomizerTypeConstants.Any);
        }

        [Test]
        public void PaladinHappens()
        {
            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.ClassName == CharacterClassConstants.Paladin);

            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Paladin));
        }

        [Test]
        public void PaladinHappensWhenLawfulGood()
        {
            var setAlignmentRandomizer = GetNewInstanceOf<ISetAlignmentRandomizer>();
            setAlignmentRandomizer.SetAlignment.Lawfulness = AlignmentConstants.Lawful;
            setAlignmentRandomizer.SetAlignment.Goodness = AlignmentConstants.Good;

            AlignmentRandomizer = setAlignmentRandomizer;

            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.ClassName == CharacterClassConstants.Paladin);

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

            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.ClassName == CharacterClassConstants.Paladin);

            Assert.That(characterClass.ClassName, Is.EqualTo(CharacterClassConstants.Paladin));
        }

        [Test]
        public void SpecialistFieldsHappen()
        {
            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.SpecialistFields.Any());

            Assert.That(characterClass.SpecialistFields, Is.Not.Empty, characterClass.ClassName);
        }

        [Test]
        public void SpecialistFieldsDoNotHappen()
        {
            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.SpecialistFields.Any() == false);

            Assert.That(characterClass.SpecialistFields, Is.Empty, characterClass.ClassName);
        }

        [Test]
        public void ProhibitedFieldsHappen()
        {
            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.ProhibitedFields.Any());

            Assert.That(characterClass.ProhibitedFields, Is.Not.Empty, characterClass.ClassName);
        }

        [Test]
        public void ProhibitedFieldsDoNotHappen()
        {
            var characterClass = Generate<CharacterClass>(GenerateClass,
                c => c.ProhibitedFields.Any() == false);

            Assert.That(characterClass.ProhibitedFields, Is.Empty, characterClass.ClassName);
        }
    }
}