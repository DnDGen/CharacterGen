using System;
using System.Collections.Generic;
using NPCGen.Selectors.Interfaces;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors
{
    public class FeatsSelector : IFeatsSelector
    {
        public IEnumerable<RacialFeatSelection> SelectRacial()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdditionalFeatSelection> SelectAdditional()
        {
            throw new NotImplementedException();
        }
    }
}