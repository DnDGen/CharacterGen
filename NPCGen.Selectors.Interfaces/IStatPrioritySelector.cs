using System;
using NPCGen.Common;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatPrioritySelector
    {
        StatPriority GetStatPriorities(String className);
    }
}