using System;
using NPCGen.Common;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatPrioritySelector
    {
        StatPriority GetStatPrioritiesFor(String className);
    }
}