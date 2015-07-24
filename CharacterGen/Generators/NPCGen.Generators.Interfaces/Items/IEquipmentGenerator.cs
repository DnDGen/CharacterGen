using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Items;

namespace NPCGen.Generators.Interfaces.Items
{
    public interface IEquipmentGenerator
    {
        Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass);
    }
}