using CharacterGen.Generators.Combats;
using NUnit.Framework;
using System;

namespace CharacterGen.Tests.Unit.Generators.Combats
{
    [TestFixture]
    public class CombatGeneratorTypeConstantsTests
    {
        [TestCase(CombatGeneratorTypeConstants.Animal, "Animal")]
        [TestCase(CombatGeneratorTypeConstants.Character, "Character")]
        public void Constant(String constant, String value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }
    }
}
