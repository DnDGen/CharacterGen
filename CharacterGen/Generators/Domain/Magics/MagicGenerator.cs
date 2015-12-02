using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using CharacterGen.Generators.Magics;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Magics
{
    public class MagicGenerator : IMagicGenerator
    {
        private ISpellsGenerator spellsGenerator;
        private IAnimalGenerator animalGenerator;

        public MagicGenerator(ISpellsGenerator spellsGenerator, IAnimalGenerator animalGenerator)
        {
            this.spellsGenerator = spellsGenerator;
            this.animalGenerator = animalGenerator;
        }

        public Magic GenerateWith(Alignment alignment, CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, IEnumerable<Feat> feats)
        {
            var magic = new Magic();
            magic.SpellsPerDay = spellsGenerator.GenerateFrom(characterClass, stats);
            magic.Animal = animalGenerator.GenerateFrom(alignment, characterClass, race, feats);

            return magic;
        }
    }
}
