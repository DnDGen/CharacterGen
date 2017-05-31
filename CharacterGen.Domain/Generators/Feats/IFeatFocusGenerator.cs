using CharacterGen.CharacterClasses;
using CharacterGen.Domain.Selectors.Selections;
using CharacterGen.Feats;
using CharacterGen.Skills;
using System.Collections.Generic;

namespace CharacterGen.Domain.Generators.Feats
{
    internal interface IFeatFocusGenerator
    {
        string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        string GenerateFrom(string feat, string focusType, IEnumerable<Skill> skills);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills, IEnumerable<RequiredFeatSelection> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        string GenerateAllowingFocusOfAllFrom(string feat, string focusType, IEnumerable<Skill> skills);
    }
}