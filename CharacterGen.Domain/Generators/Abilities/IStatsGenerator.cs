using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Races;
using CharacterGen.Randomizers.Stats;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities
{
    internal interface IStatsGenerator
    {
        Dictionary<string, Stat> GenerateWith(IStatsRandomizer statsRandomizer, CharacterClass characterClass, Race race);
    }
}