using CharacterGen.Generators.Randomizers.CharacterClasses;
using NUnit.Framework;

namespace CharacterGen.Tests.Unit.Generators.Randomizers.CharacterClasses.ClassNames
{
    [TestFixture]
    public class ClassNameRandomizerTypeConstantsTests
    {
        [TestCase(ClassNameRandomizerTypeConstants.AnyPlayer, "Any Player")]
        [TestCase(ClassNameRandomizerTypeConstants.AnyNPC, "Any NPC")]
        [TestCase(ClassNameRandomizerTypeConstants.Healer, "Healer")]
        [TestCase(ClassNameRandomizerTypeConstants.Mage, "Mage")]
        [TestCase(ClassNameRandomizerTypeConstants.NonSpellcaster, "Non-spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.Spellcaster, "Spellcaster")]
        [TestCase(ClassNameRandomizerTypeConstants.Stealth, "Stealth")]
        [TestCase(ClassNameRandomizerTypeConstants.Warrior, "Warrior")]
        public void Constant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
