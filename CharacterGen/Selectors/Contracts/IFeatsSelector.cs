using System;
using System.Collections.Generic;
using CharacterGen.Selectors.Objects;

namespace CharacterGen.Selectors
{
    public interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectRacial(String race);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        IEnumerable<CharacterClassFeatSelection> SelectClass(String characterClassName);
    }
}