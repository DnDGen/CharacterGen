﻿using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Alignments;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using System.Collections.Generic;

namespace CharacterGen.Generators.Magics
{
    public interface IMagicGenerator
    {
        Magic GenerateWith(Alignment alignment, CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment);
    }
}
