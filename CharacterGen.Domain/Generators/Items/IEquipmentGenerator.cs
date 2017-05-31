using CharacterGen.CharacterClasses;
using CharacterGen.Feats;
using CharacterGen.Items;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Items
{
    internal interface IEquipmentGenerator
    {
        Equipment GenerateWith(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}