using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Generators.Interfaces.Abilities.Feats
{
    public interface IFeatFocusGenerator
    {
        String GenerateFrom(String featId, String focusType, IEnumerable<RequiredFeat> requiredFeats, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        String GenerateFrom(String featId, String focusType, Dictionary<String, Skill> skills);
    }
}