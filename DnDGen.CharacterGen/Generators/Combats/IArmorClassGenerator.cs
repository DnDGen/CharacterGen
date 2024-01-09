using DnDGen.CharacterGen.Combats;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Items;
using DnDGen.CharacterGen.Races;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Combats
{
    internal interface IArmorClassGenerator
    {
        ArmorClass GenerateWith(Equipment equipment, int adjustedDexterityBonus, IEnumerable<Feat> feats, Race race);
    }
}