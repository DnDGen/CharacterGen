using CharacterGen.Abilities;
using CharacterGen.CharacterClasses;
using CharacterGen.Combats;
using CharacterGen.Feats;
using CharacterGen.Races;
using CharacterGen.Skills;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
{
    internal interface IFeatsGenerator
    {
        FeatCollections GenerateWith(CharacterClass characterClass, Race race, Dictionary<string, Ability> abilities, IEnumerable<Skill> skills, BaseAttack baseAttack);
    }
}