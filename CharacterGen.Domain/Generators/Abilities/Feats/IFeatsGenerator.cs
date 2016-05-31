using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Races;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal interface IFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Stat> stats, Dictionary<string, Skill> skills,
            BaseAttack baseAttack);
    }
}