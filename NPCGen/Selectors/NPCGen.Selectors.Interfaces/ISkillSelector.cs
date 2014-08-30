using System;
using NPCGen.Selectors.Interfaces.Objects;

namespace NPCGen.Selectors.Interfaces
{
    public interface ISkillSelector
    {
        SkillSelection SelectFor(String skill);
    }
}