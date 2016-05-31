using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;
using TreasureGen.Common.Items;

namespace CharacterGen.Domain.Generators.Items
{
    internal interface IArmorGenerator
    {
        Item GenerateArmorFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateShieldFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}
