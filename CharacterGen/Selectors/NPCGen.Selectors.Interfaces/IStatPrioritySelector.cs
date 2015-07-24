using System;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface IStatPrioritySelector
    {
        StatPrioritySelection SelectFor(String className);
    }
}