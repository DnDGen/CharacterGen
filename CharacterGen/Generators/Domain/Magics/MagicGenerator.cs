using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;
using CharacterGen.Generators.Magics;
using System;
using System.Collections.Generic;

namespace CharacterGen.Generators.Domain.Magics
{
    public class MagicGenerator : IMagicGenerator
    {
        public Magic GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment)
        {
            throw new NotImplementedException();
        }
    }
}
