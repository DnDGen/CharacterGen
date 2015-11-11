using CharacterGen.Common.CharacterClasses;
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