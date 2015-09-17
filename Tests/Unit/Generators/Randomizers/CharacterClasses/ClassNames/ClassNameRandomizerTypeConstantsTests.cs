using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class ClassNameRandomizerTypeConstantsTests
    {
        [TestCase(ClassNameRandomizerTypeConstants.Any, "Any")]
        [TestCase(ClassNameRandomizerTypeConstants.Healer, "Healer")]
        [TestCase(ClassNameRandomizerTypeConstants.Mage, "Mage")]
        [TestCase(ClassNameRandomizerTypeConstants.NonSpellcaster, "Non-spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.Spellcaster, "Spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.Stealth, "Stealth")]
        [TestCase(ClassNameRandomizerTypeConstants.Warrior, "Warrior")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
