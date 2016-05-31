using CharacterGen.Domain.Selectors.Selections;
using System.Collections.Generic;

namespace CharacterGen.Domain.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectRacial(string race);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        IEnumerable<CharacterClassFeatSelection> SelectClass(string characterClassName);
    }
}