using System;
using System.Collections.Generic;
using CharacterGen.Common.Races;

namespace CharacterGen.Selectors
{
    public interface IStatAdjustmentsSelector
    {
        Dictionary<String, Int32> SelectFor(Race race);
    }
}