using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Stats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface IMagicGenerator
    {
        Magic GenerateWith(Alignment alignment, CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, IEnumerable<Feat> feats, Equipment equipment);
    }
}
