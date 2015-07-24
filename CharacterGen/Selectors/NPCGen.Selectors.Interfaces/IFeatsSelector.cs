using System;
using System.Collections.Generic;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectRacial(String race);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        IEnumerable<CharacterClassFeatSelection> SelectClass(String characterClassName);
    }
}