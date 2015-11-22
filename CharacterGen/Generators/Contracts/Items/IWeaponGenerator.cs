using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Common.Races;
using System;
using System.Collections.Generic;
using TreasureGen.Common.Items;

namespace CharacterGen.Generators.Items
{
    public interface IWeaponGenerator
    {
        Item GenerateFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateAmmunition(IEnumerable<Feat> feats, CharacterClass characterClass, Race race, String ammunitionType);
        Item GenerateMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateOneHandedMeleeFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
        Item GenerateRangedFrom(IEnumerable<Feat> feats, CharacterClass characterClass, Race race);
    }
}