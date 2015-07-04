using System;
using System.Collections.Generic;
using NPCGen.Common.Abilities.Feats;
using NPCGen.Common.Abilities.Skills;
using NPCGen.Common.CharacterClasses;

namespace NPCGen.Generators.Interfaces.Abilities.Feats
{
    public interface IFeatFocusGenerator
    {
        String GenerateFrom(String featId, String focusType, IEnumerable<String> requiredFeatIds, IEnumerable<Feat> otherFeat, CharacterClass characterClass);
        String GenerateFrom(String featId, String focusType, Dictionary<String, Skill> skills);
    }
}