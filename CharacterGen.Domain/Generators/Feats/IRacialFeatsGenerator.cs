using CharacterGen.Abilities;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
{
    internal interface IRacialFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(Race race, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
    }
}