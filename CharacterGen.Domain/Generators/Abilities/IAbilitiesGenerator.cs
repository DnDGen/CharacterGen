using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Abilities;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Dictionary<string, Ability> GenerateWith(IAbilitiesRandomizer abilitiesRandomizer, CharacterClass characterClass, Race race);
    }
}