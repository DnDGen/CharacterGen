using DnDGen.CharacterGen.Selectors.Selections;
using System.Collections.Generic;

namespace DnDGen.CharacterGen.Selectors.Collections
{
    internal interface IFeatsSelector
    {
        IEnumerable<RacialFeatSelection> SelectRacial(string race);
        IEnumerable<AdditionalFeatSelection> SelectAdditional();
        IEnumerable<CharacterClassFeatSelection> SelectClass(string characterClassName);
    }
}