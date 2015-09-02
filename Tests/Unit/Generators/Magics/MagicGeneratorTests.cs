using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Generators.Domain.Magics;
using CharacterGen.Generators.Magics;
using NUnit.Framework;
using System.Collections.Generic;

namespace CharacterGen.Tests.Unit.Generators.Magics
{
    [TestFixture]
    public class MagicGeneratorTests
    {
        private IMagicGenerator magicGenerator;
        private CharacterClass characterClass;
        private List<Feat> feats;
        private Equipment equipment;

        [SetUp]
        public void Setup()
        {
            magicGenerator = new MagicGenerator();
            characterClass = new CharacterClass();
            feats = new List<Feat>();
            equipment = new Equipment();
        }

        [Test]
        public void GenerateMagic()
        {
            var magic = magicGenerator.GenerateWith(characterClass, feats, equipment);
            Assert.That(magic, Is.Not.Null);
        }
    }
}
