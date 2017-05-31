using CharacterGen.Feats;
using CharacterGen.Abilities;
using CharacterGen.Alignments;
using CharacterGen.CharacterClasses;
using CharacterGen.Items;
using CharacterGen.Magics;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Magics
{
    internal interface IMagicGenerator
    {
        Magic GenerateWith(Alignment alignment, CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Feat> feats, Equipment equipment);
    }
}
