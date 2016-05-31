using CharacterGen.Abilities.Feats;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Magics
{
    internal interface IAnimalGenerator
    {
        string GenerateFrom(Alignment alignment, CharacterClass characterClass, Race race, IEnumerable<Feat> feats);
    }
}
