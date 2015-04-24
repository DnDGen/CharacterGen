using System;
using System.Collections.Generic;
using NPCGen.Common.CharacterClasses;
using NPCGen.Common.Races;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectFor(Race race);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        AdditionalFeatSelection SelectAdditional(String featId);
        IEnumerable<CharacterClassFeatSelection> SelectFor(CharacterClass characterClass);
    }
}