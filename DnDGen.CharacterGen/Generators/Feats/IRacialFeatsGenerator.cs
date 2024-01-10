using DnDGen.CharacterGen.Abilities;
using DnDGen.CharacterGen.Feats;
using DnDGen.CharacterGen.Races;
using DnDGen.CharacterGen.Skills;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Generators.Feats
{
    internal interface IRacialFeatsGenerator
    {
        IEnumerable<Feat> GenerateWith(Race race, IEnumerable<Skill> skills, Dictionary<string, Ability> abilities);
    }
}