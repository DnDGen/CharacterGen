using CharacterGen.Generators.Abilities;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Abilities
{
    [TestFixture]
    public class AbilitiesGeneratorTypeConstantsTests
    {
        [TestCase(AbilitiesGeneratorTypeConstants.Animal, "Animal")]
        [TestCase(AbilitiesGeneratorTypeConstants.Character, "Character")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
