using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;

namespace CharacterGen.Generators.Items
{
    public interface IEquipmentGenerator
    {
        Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass);
    }
}