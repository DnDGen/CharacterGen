using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface IAnimalGenerator
    {
        //Animal GenerateFrom(Alignment alignment, CharacterClass characterClass, Race race, IEnumerable<Feat> feats);
        String GenerateFrom(Alignment alignment, CharacterClass characterClass, Race race, IEnumerable<Feat> feats);
    }
}
