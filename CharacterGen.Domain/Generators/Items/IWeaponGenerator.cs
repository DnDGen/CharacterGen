using CharacterGen.Abilities.Feats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using System.Collections.Generic;
using TreasureGen.Items;

namespace CharacterGen.Domain.Generators.Items
{
    internal interface IWeaponGenerator
    {
        Weapon GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Weapon GenerateAmmunition(CharacterClass characterClass, Race race, string ammunitionType);
        Weapon GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Weapon GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Weapon GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}