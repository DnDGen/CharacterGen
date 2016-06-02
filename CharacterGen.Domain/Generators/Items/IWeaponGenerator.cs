using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Items
{
    internal interface IWeaponGenerator
    {
        Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateAmmunition(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, string ammunitionType);
        Item GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}