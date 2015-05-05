using System;
using System.Collections.Generic;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectRacial(String raceId);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        AdditionalFeatSelection SelectAdditional(String featId);
        IEnumerable<CharacterClassFeatSelection> SelectClass(String characterClassName);
    }
}