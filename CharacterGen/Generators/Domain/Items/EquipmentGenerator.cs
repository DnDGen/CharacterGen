using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Generators.Items;

namespace CharacterGen.Generators.Domain.Items
{
    public class TreasureGenerator : ITreasureGenerator
    {
        public Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass)
        {
            throw new NotImplementedException();
        }
    }
}