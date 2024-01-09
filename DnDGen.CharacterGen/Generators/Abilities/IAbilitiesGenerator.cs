using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.CharacterClasses;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Randomizers.Abilities;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Dictionary<string, Ability> GenerateWith(IAbilitiesRandomizer abilitiesRandomizer, CharacterClass characterClass, Race race);
    }
}