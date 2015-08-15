using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using System.Collections.Generic;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Items
{
    public interface GearGenerator
    {
        Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass);
    }
}