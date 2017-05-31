using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
{
    internal interface IClassFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Feat> racialFeats, IEnumerable<Skill> skills);
    }
}