using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Combats;
using CharacterGen.Common.Items;
using CharacterGen.Common.Races;

namespace CharacterGen.Generators.Combats
{
    public interface IArmorClassGenerator
    {
        ArmorClass GenerateWith(Equipment equipment, Int32 adjustedDexterityBonus, IEnumerable<Feat> feats, Race race);
    }
}