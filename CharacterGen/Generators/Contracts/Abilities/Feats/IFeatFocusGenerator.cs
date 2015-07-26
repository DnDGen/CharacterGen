using System;
using System.Collections.Generic;
using CharacterGen.Common.Abilities.Feats;
using CharacterGen.Common.Abilities.Skills;
using CharacterGen.Common.CharacterClasses;
using CharacterGen.Selectors.Objects;

namespace CharacterGen.Generators.Abilities.Feats
{
    public interface IFeatFocusGenerator
    {
        String GenerateFrom(String feat, String focusType, Dictionary<String, Skill> skills, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        String GenerateFrom(String feat, String focusType, Dictionary<String, Skill> skills);
        String GenerateAllowingFocusOfAllFrom(String feat, String focusType, Dictionary<String, Skill> skills, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        String GenerateAllowingFocusOfAllFrom(String feat, String focusType, Dictionary<String, Skill> skills);
    }
}