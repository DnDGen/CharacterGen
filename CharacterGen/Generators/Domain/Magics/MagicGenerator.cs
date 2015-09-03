using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Generators.Magics;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Magics
{
    public class MagicGenerator : Generator, IMagicGenerator
    {
        private ISpellsGenerator spellsGenerator;
        private IAnimalGenerator animalGenerator;

        public MagicGenerator(ISpellsGenerator spellsGenerator, IAnimalGenerator animalGenerator)
        {
            this.spellsGenerator = spellsGenerator;
            this.animalGenerator = animalGenerator;
        }

        public Magic GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment)
        {
            var magic = new Magic();
            magic.Spells = spellsGenerator.GenerateFrom(characterClass, feats, equipment);
            magic.Animal = animalGenerator.GenerateFrom(characterClass, feats);

            return magic;
        }
    }
}
