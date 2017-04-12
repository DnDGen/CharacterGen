using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Items
{
    internal interface IArmorGenerator
    {
        Armor GenerateArmorFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Armor GenerateShieldFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}
