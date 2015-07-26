using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Magics;

namespace CharacterGen.Generators.Magics
{
    public interface IMagicGenerator
    {
        Magic GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment);
    }
}
