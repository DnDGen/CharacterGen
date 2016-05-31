using CharacterGen.Abilities.Feats;
using CharacterGen.Abilities.Skills;
using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Selections;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Abilities.Feats
{
    internal interface IFeatFocusGenerator
    {
        string GenerateFrom(string feat, string focusType, Dictionary<string, Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        string GenerateFrom(string feat, string focusType, Dictionary<string, Skill> skills);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, Dictionary<string, Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, Dictionary<string, Skill> skills);
    }
}