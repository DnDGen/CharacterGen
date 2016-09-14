using CharacterGen.Abilities;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal interface IAbilitiesGenerator
    {
        Ability GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, BaseAttack baseAttack);
        Dictionary<string, Stat> GenerateStats(CharacterClass characterClass, Race race, IStatsRandomizer statsRandomizer);
    }
}