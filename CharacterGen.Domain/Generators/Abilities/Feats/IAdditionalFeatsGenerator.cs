using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.Abilities.Stats;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Races;
using System;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal interface IAdditionalFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<String, Stat> stats, Dictionary<String, Skill> skills,
            BaseAttack baseAttack, IEnumerable<Feat> preselectedFeats);
    }
}