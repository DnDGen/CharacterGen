using System;
using CharacterGen.Selectors.Objects;

namespace CharacterGen.Selectors
{
    public interface IStatPrioritySelector
    {
        StatPrioritySelection SelectFor(String className);
    }
}