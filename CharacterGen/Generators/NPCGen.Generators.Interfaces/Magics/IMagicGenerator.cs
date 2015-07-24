using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;
using NPCGen.Common.Magics;

namespace NPCGen.Generators.Interfaces.Magics
{
    public interface IMagicGenerator
    {
        Magic GenerateWith(CharacterClass characterClass, IEnumerable<Feat> feats, Equipment equipment);
    }
}
