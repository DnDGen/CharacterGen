using System;
using System.Collections.Generic;
using NPCGen.Common.Races;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatAdjustmentsSelector
    {
        Dictionary<String, Int32> SelectAdjustmentsFor(Race race);
    }
}