using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;
using System.Collections.Generic;

namespace CharacterGen.Generators.Items
{
    public interface IEquipmentGenerator
    {
        Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}