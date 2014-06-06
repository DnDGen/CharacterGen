using System;
using NPCGen.Common;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatPriorityProvider
    {
        StatPriority GetStatPriorities(String className);
    }
}