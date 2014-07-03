using System;
using NPCGen.Common;
using NPCGen.Common.Abilities.Stats;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatPrioritySelector
    {
        StatPriority GetStatPrioritiesFor(String className);
    }
}