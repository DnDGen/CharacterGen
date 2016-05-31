using CharacterGen.Abilities.Feats;
using CharacterGen.Combats;
using CharacterGen.Items;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Combats
{
    internal interface IArmorClassGenerator
    {
        ArmorClass GenerateWith(Equipment equipment, int adjustedDexterityBonus, IEnumerable<Feat> feats, Race race);
    }
}